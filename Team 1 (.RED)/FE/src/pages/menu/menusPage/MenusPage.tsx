import { useCallback, useContext, useEffect, useRef } from "react";
import { allMenusContext } from "./MenusPage.store";
import styles from "./MenusPage.module.scss";
import { Box } from "@mui/material";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react";
import { MenuCard } from "./components/MenuCard";
import { MenuCategory } from "../../../shared/Types";
import { LoadingPage } from "./components/LoadingPage";

const getMenuCategory = (stringValue?: string) => {
    if (!stringValue) {
        return null;
    }

    return MenuCategory[stringValue as any] as unknown as MenuCategory ?? null;
}

export const MenusPage = observer(() => {
    const { menuCategory } = useParams();
    const { 
        menus,
        getMenusByCategory,
        isLoading,
        errorMessage,
        setErrorMessage,
        pageNumber,
        setNextPage,
        hasMore,
        resetPage: resetPageNumber,
    } = useContext(allMenusContext);

    useEffect(() => {
        const category = getMenuCategory(menuCategory);
        if (!category) {
            setErrorMessage("Invalid category!");
            return;
        }
        getMenusByCategory(category as number);
    }, [menuCategory, getMenusByCategory, setErrorMessage, pageNumber]);

    const observer = useRef<any>();

    const lastElementReference = useCallback((node: any) => {
        if (isLoading) return;

        if (observer.current) observer.current.disconnect();
        observer.current = new IntersectionObserver(entries => {
          if (entries[0].isIntersecting && hasMore) {
            setNextPage();
          }
        });

        if (node) observer.current.observe(node);
      }, [isLoading, hasMore, setNextPage]);

    useEffect(() => resetPageNumber, [resetPageNumber]);

    if (!!errorMessage) {
        return (
            <Box className={styles.container}>
                <p>{errorMessage}</p>
            </Box>
        );
    }

    return (
        <div className={styles.container}>
            {menus.map((menu) => (
                <MenuCard menu={menu} key={menu.id} />
            ))}
            {isLoading && <LoadingPage />}
            <div ref={lastElementReference} className={styles.loader}></div>
        </div>
    );
});