import { MenuItem, Select } from "@mui/material";
import { IngredientType } from "../../AddRecipePage.types";
import { observer } from "mobx-react";

interface Props {
    ingredientsList: IngredientType[];
    label: string;
    onChange: (ingredientId: number) => void;
}

export const IngredientsSelect = observer(({ ingredientsList, label, onChange }: Props) => (
    <Select label={label} onChange={({ target }) => onChange(target.value as unknown as number)} value="">
        {ingredientsList.map(({ id, name }) => (
            <MenuItem value={id} key={id}>{name}</MenuItem>
        ))}
    </Select>
));