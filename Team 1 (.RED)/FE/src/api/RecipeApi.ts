import { PaginationInformation, Recipe, RecipeRecords, RecipeObject } from "../shared/Types";
import { getAuthorizationHeaders } from "../shared/Authorization";
import { BaseApi } from "./BaseApi";

const API_PATH = "/api/recipe";

export const getAllRecipes = async (paginationInformation: PaginationInformation): Promise<RecipeRecords> => {
    const response = await BaseApi.post(`${API_PATH}/get-unused-recipes`, paginationInformation, getAuthorizationHeaders());
    const data = response.data;
    return data;
}

export const getRecipe = async (recipeId: string) : Promise<Recipe> => {
    const response = await BaseApi.get(`${API_PATH}/get-recipe/${recipeId}`, getAuthorizationHeaders());
    return response.data;
}

export const addRecipe = async (recipeObject: RecipeObject) => {
    await BaseApi.post(`${API_PATH}/add-recipe`, recipeObject, getAuthorizationHeaders());
}