import { makeAutoObservable, runInAction } from "mobx";
import { createContext } from "react";
import { Recipe } from "../../shared/Types";
import { getAllRecipes } from "../../api/RecipeApi";
import { paginationStore } from "./components/Pagination.store";

export class AllRecipesStore {
    public recipes: Recipe[] = [];
    public isLoading = true;
    public error = null;

    constructor () {
        makeAutoObservable(this);
    }

    public getAllRecipes = async () => {
        try {
            const responseData = await getAllRecipes(paginationStore.getInformation());
            runInAction(() => {
                this.recipes = responseData.items;
                paginationStore.setTotalCount(responseData.totalRecords);
            });
        } catch (error: any) {
            runInAction(() => {
                this.error = error.message;
            });
        } finally {
            runInAction(() => {
                this.isLoading = false;
            });
        }
    }
}

export const allRecipesStore = new AllRecipesStore();
export const allRecipesContext = createContext(allRecipesStore);