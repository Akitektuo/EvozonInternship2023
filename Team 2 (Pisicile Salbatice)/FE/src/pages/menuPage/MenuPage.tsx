import { Paper } from "@mui/material";
import { useContext, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { MenuPageContext } from "./MenuPage.store";
import { RoutesConstants } from "../../shared/RoutesConstants";
import { observer } from "mobx-react";
import { MealsList } from "./components/MealsList";
import { MenuDetails } from "./components/MenuDetails";
import styles from "./MenuPage.module.scss";

export const MenuPage = observer(() => {
    const { menuData, fetchMenu } = useContext(MenuPageContext);
    const { menuId } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        fetchMenu(Number(menuId), () => navigate(RoutesConstants.BaseRoute));
    }, [fetchMenu, navigate, menuId]);

    if (!menuData) {
        return null;
    }

    return (
        <Paper elevation={3} className={styles.menuPageContainer}>
            <MenuDetails menuData={menuData} />
            <MealsList meals={menuData.meals} />
        </Paper>
    )
});