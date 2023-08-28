import { MenuCategory, SuggestedMenu } from "../../../../shared/Types"
import styles from "../../components/MenuDetails.module.scss";
// import styles from "./SuggestedMenuDetails.module.scss";
import { MoreHorizTwoTone, NotesTwoTone } from "@mui/icons-material";

interface Props {
    suggestedMenu: SuggestedMenu
}

export const SuggestedMenuDetails = ({ suggestedMenu }: Props) => (
    <div className={styles.menuContainer}>
        <div className={styles.menuCard}>
            <div>
                <h3><NotesTwoTone className={styles.icon} />Name: </h3>
                {suggestedMenu?.name}
            </div>
            <div>
                <h3>Category</h3>
                <p>{MenuCategory[suggestedMenu.categoryId]}</p>
            </div>
            <div>
                <h3><MoreHorizTwoTone className={styles.icon}/> Description:</h3>
                <p>{suggestedMenu?.description}</p>
            </div>
            <div>
                <h3>Price</h3>
                <p>&euro; {suggestedMenu?.totalPrice}</p>
            </div>
        </div>
    </div>
)