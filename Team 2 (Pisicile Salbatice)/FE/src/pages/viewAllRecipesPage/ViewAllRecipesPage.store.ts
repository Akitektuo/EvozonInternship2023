import { makeAutoObservable, runInAction } from "mobx";
import { createContext } from "react";
import { RecipeListType } from "./ViewAllRecipesPage.types";
import { postAllRecipes } from "../../api/RecipeApi";
import { DEFAULT_PAGINATION_MODEL, PaginationOptionsType } from "../../shared/Pagination.types";

export class ViewAllRecipesPageStore {
    public paginationData: PaginationOptionsType = DEFAULT_PAGINATION_MODEL;

    public recipeList: RecipeListType | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    public getAllRecipes = async (navigate: () => void, page: number = 1) => {
        try {
            this.paginationData.paginationModel.pageNumber = page;
            const recipeList = await postAllRecipes(this.paginationData);
            runInAction(() => this.recipeList = recipeList);
        } catch(error) {
            navigate();
        }
    };
}

export const viewAllRecipesPageStore = new ViewAllRecipesPageStore();
export const ViewAllRecipesPageContext = createContext(viewAllRecipesPageStore);