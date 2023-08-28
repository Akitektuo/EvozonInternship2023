import { useCallback, useContext, useEffect } from "react"
import { GeneratedMenuPageContext } from "./GeneratedMenuPage.store"
import { observer } from "mobx-react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { CategoriesEnum } from "../../shared/CategoriesEnum";
import { RoutesConstants } from "../../shared/RoutesConstants";
import { Backdrop, CircularProgress, Fab, Paper } from "@mui/material";
import { MealsList } from "../menuPage/components/MealsList";
import { MenuDetails } from "../menuPage/components/MenuDetails";
import styles from "./GeneratedMenuPage.module.scss";

export const GeneratedMenuPage = observer(() => {
    const { displayMenu, isLoading, fetchMenu, reset, postMenu } = useContext(GeneratedMenuPageContext);
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();

    const goBackToForm = useCallback(() => navigate(RoutesConstants.GenerateMenusRoute), [navigate]);

    useEffect(() => {
        const menuType = Number(searchParams.get("menuType")) as CategoriesEnum;
        const priceSuggestion = Number(searchParams.get("priceSuggestion"));

        fetchMenu(menuType, priceSuggestion, goBackToForm);

        return reset;
    }, [searchParams, fetchMenu, navigate, reset, goBackToForm]);

    return <>
        {displayMenu && <>
            <Paper elevation={3} className={styles.container}>
                <MenuDetails menuData={displayMenu} />
                <MealsList meals={displayMenu.meals} />
            </Paper>
            <Fab variant="extended" color="primary" className={styles.actionButton} onClick={() => postMenu(goBackToForm)}>
                Save menu
            </Fab>
        </>}
        <Backdrop className={styles.elevatedLoadingOverlay} open={isLoading}>
            <CircularProgress />
        </Backdrop>
    </>;
});