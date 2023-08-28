import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { RecipeType } from "./RecipePage.types";
import { getRecipe } from "../../api/RecipeApi";

export class RecipePageStore {
    public recipeData: RecipeType | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    public fetchRecipe = async (id: number, navigateTo: () => void) => {
        try {
            const recipe = await getRecipe(id);
            this.recipeData = recipe;
        } catch (error) {
            navigateTo();
        }
    }
}

export const recipePageStore = new RecipePageStore();
export const RecipePageContext = createContext(recipePageStore);