import { Chip } from "@mui/material";
import { IngredientType } from "../../AddRecipePage.types";
import styles from "./ChipItemsList.module.scss";
import { observer } from "mobx-react";

interface Props {
    selectedIngredients: IngredientType[];
    onDelete: (ingredient: IngredientType) => void;
}

export const ChipItemsList = observer(({ selectedIngredients, onDelete }: Props) => <>
    {selectedIngredients.map(ingredient => (
        <Chip 
            className={styles.chipItem} 
            key={ingredient.id} 
            label={ingredient.name} 
            onDelete={() => onDelete(ingredient)} />
    ))}
</>);