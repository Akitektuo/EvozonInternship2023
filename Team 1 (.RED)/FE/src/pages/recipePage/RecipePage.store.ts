import { makeAutoObservable, runInAction } from "mobx";
import { Recipe } from "../../shared/Types";
import { getRecipe } from "../../api/RecipeApi";
import { popUpNotificationStore } from "../../services/popUpNotifications/PopUpNotifications.store";
import { createContext } from "react";

export class RecipePageStore {
    public recipe: Recipe | null = null;
    public isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    public getRecipeById = async (recipeId: string) => {
        this.isLoading = true;
        try {
            const responseData = await getRecipe(recipeId);
            runInAction(() => {
                this.recipe = responseData;
            });
        } catch (error) {
            popUpNotificationStore.handleError(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    }

    public resetRecipe() { 
        this.recipe = null;
    }
}

export const recipePageStore = new RecipePageStore();
export const recipePageContext = createContext(recipePageStore);