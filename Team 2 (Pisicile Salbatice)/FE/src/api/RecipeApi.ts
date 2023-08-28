import { RecipeType } from "../pages/addRecipePage/AddRecipePage.types";
import { RecipeListType } from "../pages/viewAllRecipesPage/ViewAllRecipesPage.types";
import { PaginationOptionsType } from "../shared/Pagination.types";
import { BaseApi, getAuthorizationHeader } from "./BaseApi";

const API_PATH = "api/recipe";

export const getRecipe = async (id: number) => {
    const { data } = await BaseApi.get(`${API_PATH}/get-recipe/${id}`, getAuthorizationHeader());
    return data;
}

export const postAllRecipes = async (paginationModel: PaginationOptionsType): Promise<RecipeListType> => {
    const { data } = await BaseApi.post(`${API_PATH}/get-recipes`, paginationModel, getAuthorizationHeader());
    return data;
}

export const postRecipe = async (recipe: RecipeType) => {
    await BaseApi.post(`${API_PATH}/add-recipe`, recipe, getAuthorizationHeader());
}

export const getIngredients = async () => {
    const { data } = await BaseApi.get("api/ingredient/get-ingredients", getAuthorizationHeader());
    return data;
}