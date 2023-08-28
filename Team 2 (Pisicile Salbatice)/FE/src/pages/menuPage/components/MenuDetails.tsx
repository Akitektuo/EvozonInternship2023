import { Typography } from "@mui/material";
import { MenuType } from "../MenuPage.types";
import { CategoriesEnum } from "../../../shared/CategoriesEnum";
import styles from "./MenuDetails.module.scss";

interface Props {
    menuData: MenuType;
}

export const MenuDetails = ({ menuData }: Props) => (
    <div className={styles.menuItemDescription}>
        <div className={styles.menuItemTitle}>

        <Typography variant="h2" component="h1">
            {menuData?.name}
        </Typography> 
        <Typography variant="h5" component="p">
            Category: {CategoriesEnum[menuData.category]}
        </Typography>    
        </div>
        <Typography variant="subtitle1" component="p" className={styles.priceItem}>
            Price: {Number(menuData?.price.toFixed(2))} $
        </Typography>    
    </div>
);