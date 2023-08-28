import { Button } from "@mui/material";
import styles from "./MenuCard.module.scss";
import { KeyboardDoubleArrowRight } from "@mui/icons-material";
import { Link } from "react-router-dom";
import { Euro } from "@mui/icons-material";
import { Menu } from "../../../../shared/Types";

interface Props {
    menu: Menu;
}

export const MenuCard = ({ menu }: Props) => (
    <div className={styles.menuCard}>
        <div className={styles.menuName}>
            <h3>Name</h3>
            <span className={styles.cardText}>{menu.name}</span>
        </div>
        <div className={styles.menuDescription}>
            <h3>Description</h3>
            <span className={styles.cardText}>{menu.description}</span>
        </div>
        <div className={styles.menuPrice}>
            <h4 className={styles.priceTitle}>
                <Euro className={styles.euroIcon}/> 
                {menu.price.toFixed(2)}
            </h4>
        </div>
        <div className={styles.detailsLink}>
            <Button component={Link} to={`${menu.id}`} className={styles.detailsButton}>
                <span>See Details</span>
                <KeyboardDoubleArrowRight className={styles.icon}/>
            </Button>
        </div>
    </div>
);