import { observer } from "mobx-react";
import { useCallback, useContext, useEffect } from "react";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import { CategoryPageContext } from "./MenusByCategoryPage.store";
import { RoutesConstants } from "../../shared/RoutesConstants";
import { MenuCard } from "./components/MenuCard";
import { Pagination } from "@mui/material";
import { calculatePageCount } from "../../shared/CalculatePageCount";
import styles from "./MenusByCategoryPage.module.scss";

export const CategoryPage = observer(() => { 
    const navigate = useNavigate();
    const { category } = useParams();
    const [searchParams, setSearchParams] = useSearchParams();
    
    const { getMenusByCategory, menusByCategory, paginationMenuData } = useContext(CategoryPageContext);

    const pageCount = calculatePageCount(menusByCategory?.totalMenusCount, paginationMenuData);

    const handleChange = useCallback((page: number) => {
        getMenusByCategory(() => navigate(RoutesConstants.BaseRoute), category, page);
        setSearchParams({"page": page.toString()});
        navigate(`${RoutesConstants.MenusRoute}/${category}?page=${page}`);
    }, [getMenusByCategory, navigate, category, setSearchParams]);

    useEffect(() => {
        getMenusByCategory(() => navigate(RoutesConstants.BaseRoute), category, Number(searchParams.get("page")));
    }, [getMenusByCategory, navigate, category, searchParams]);

    return (
        <div className={styles.menusPage}>
            <div className={styles.allMenusContainer}>
                {menusByCategory?.menusList.map((menu) => (
                    <MenuCard 
                        category={menu.category}
                        name={menu.name}
                        price={Number(menu.price.toFixed(2))} 
                        path={`${RoutesConstants.MenusRoute}/${category}/details/${menu.id}`}
                        key={menu.id} />
                ))}
            </div>
            <Pagination count={pageCount} page={Number(searchParams.get("page"))} onChange={(_, page) => handleChange(page)} className={styles.pagination} />
        </div>
    )
});