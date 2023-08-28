import { MenuDetails as MenuDetailsType } from "../../../shared/Types";
import { MealDetail } from "./MealDetail";
import styles from "./MenuDetails.module.scss";
import { MoreHorizTwoTone, NotesTwoTone } from "@mui/icons-material";

interface Props {
    menu: MenuDetailsType | null
}

export const MenuDetails = ({ menu }: Props) => (
    <div className={styles.menuContainer}>
        <h1>Menu Details</h1>
        <div className={styles.menuCard}>
            <div>
                <h3><NotesTwoTone className={styles.icon} />Name: </h3>
                {menu?.name}
            </div>
            <div>
                <h3><MoreHorizTwoTone className={styles.icon}/> Description:</h3>
                <p>{menu?.description}</p>
            </div>
            <div>
                <h3>Price</h3>
                <p>&euro; {menu?.price}</p>
            </div>
            <div>
                {menu?.mealsDetails.map((meal, index) => (
                    <MealDetail meal={meal} index={index} key={index} />
                ))}
            </div>
        </div>
    </div>
)