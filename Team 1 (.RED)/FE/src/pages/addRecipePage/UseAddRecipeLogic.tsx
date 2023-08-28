import { useContext } from "react";
import { addRecipePageContext } from "./AddRecipePage.store";

export const useAddRecipeLogic = () => {
    const { recipeName, recipeDescription, selectedIngredients } = useContext(addRecipePageContext);

    const checkRecipeLogic = () => {
        return !(recipeName && recipeDescription && selectedIngredients.length > 0);
    };

    return checkRecipeLogic;
}