import { observer } from "mobx-react";
import { MenuType } from "../MenusByCategoryPage.types";
import styles from "./MenuCard.module.scss";
import { Link } from "react-router-dom";

interface Props extends MenuType {
    path: string;
}

export const MenuCard = observer(({category, name, price, path}: Props) => (
    <Link className={styles.menuCard} to={path}>
        <div className={styles.namecard}>
            <div className={styles.nameTitle}>Name:</div>
            <div className={styles.nameProp}>{name}</div>
        </div>
        <div className={styles.priceCard}>
            <div className={styles.priceLine}>
                <div className={styles.priceTitle}>Price:</div>
                <div className={styles.priceProp}>{Number(price.toFixed(2))} $</div>
            </div>
            <div className={styles.categoryCard}>{category}</div>
        </div>
    </Link>
));