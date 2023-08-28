import { makeAutoObservable, runInAction } from "mobx";
import { createContext } from "react";
import { EMPTY_RECIPE, EMPTY_IS_TOUCHED, IngredientType, IsTouchedType, RecipeType } from "./AddRecipePage.types";
import { getIngredients, postRecipe } from "../../api/RecipeApi";
import { serverErrorHandlingStore } from "../authenticationPages/components/serverErrorHandling/ServerErrorHandling.store";

export class AddRecipePageStore {
    public recipe: RecipeType = EMPTY_RECIPE;
    public ingredientsList: IngredientType[] = [];
    public selectedIngredients: IngredientType[] = [];
    public recipeIsTouched: IsTouchedType = EMPTY_IS_TOUCHED;

    constructor() {
        makeAutoObservable(this);
    }

    public setRecipeName = (value: string) => this.recipe.name = value;

    public setRecipeDescription = (value: string) => this.recipe.description = value;

    public setNameIsTouched = () => this.recipeIsTouched.nameIsTouched = true;

    public setDescriptionIsTouched = () => this.recipeIsTouched.descriptionIsTouched = true;

    public setIngredientIdsIsTouched = () => this.recipeIsTouched.ingredientIdsIsTouched = true;

    public addIngredient = (ingredientId: number) => {
        const ingredient = this.ingredientsList.find(({ id }) => id === ingredientId);
        if (!ingredient || this.selectedIngredients.includes(ingredient)) {
            return;
        }

        this.selectedIngredients.push(ingredient);
        this.ingredientsList.remove(ingredient);
        this.recipe.ingredientIds.push(ingredient.id);
    }

    public removeIngredient = (ingredient: IngredientType) => {
        this.selectedIngredients.remove(ingredient);
        this.ingredientsList.push(ingredient);
        this.recipe.ingredientIds.remove(ingredient.id);
    }
    
    public reset = () => {
        this.recipe = EMPTY_RECIPE;
        this.selectedIngredients = [];
        this.recipeIsTouched = EMPTY_IS_TOUCHED;
    }

    public createRecipe = async () => {
        try {
            await postRecipe(this.recipe);
            return true;
        } catch (error: any) {
            serverErrorHandlingStore.setServerErrorMessage(error);
            return false;
        }
    }

    public fetchIngredients = async () => {
        try {
            const ingredientsList = await getIngredients();
            runInAction(() => this.ingredientsList = ingredientsList.ingredientsList);
        } catch (error: any) {
            serverErrorHandlingStore.setServerErrorMessage(error);
        }
    }
}

export const addRecipePageStore = new AddRecipePageStore();
export const AddRecipePageContext = createContext(addRecipePageStore);