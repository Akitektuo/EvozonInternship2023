import { makeAutoObservable, runInAction } from "mobx";
import { createContext } from "react";
import { getAllIngredients } from "../../api/IngredientsApi";
import { addRecipe } from "../../api/RecipeApi";
import { Ingredient, RecipeObject, EmptyNewRecipeObject } from "../../shared/Types";
import { popUpNotificationStore } from "../../services/popUpNotifications/PopUpNotifications.store";

export class AddRecipePageStore {
    public inputValue = "";
    public listOfIngredients: Ingredient[] = [];
    public filteredListOfIngredients: Ingredient[] = [];
    public error = null;
    public selectedIngredients: Ingredient[] = [];
    public recipeName = "";
    public recipeDescription = "";
    public newRecipeObject: RecipeObject = EmptyNewRecipeObject;

    constructor() {
        makeAutoObservable(this);
    }

    public handleInputValue = async (value: string) => {
        this.inputValue = value;
        this.filteredListOfIngredients = this.listOfIngredients.filter((ingredient) => {
            return ingredient.name.toLowerCase().includes(value.toLowerCase())
        });
    }

    public getIngredients = async () => {
        try {
            const responseData = await getAllIngredients();
            runInAction(() => {
                this.listOfIngredients = responseData;
            });
        } catch (error: any) {
            runInAction(() => {
                this.error = error.message;
            });
        }
    }

    public setIngredientsIdsList = () => {
        this.newRecipeObject.ingredientIds = this.selectedIngredients.map(element => element.id);
    }

    public addIngredient = (ingredient: Ingredient) => {
        if (!this.selectedIngredients.includes(ingredient)) {
            this.selectedIngredients.push(ingredient);
        }
        this.setIngredientsIdsList();
    }

    public removeIngredient = (ingredient: Ingredient) => {
        const index = this.selectedIngredients.indexOf(ingredient);
        this.selectedIngredients.splice(index, 1);
        this.setIngredientsIdsList();
    }

    public setRecipeName = (name: string) => {
        this.recipeName = name;
        this.newRecipeObject.name = this.recipeName;
    }

    public setRecipeDescription = (description: string) => {
        this.recipeDescription = description;
        this.newRecipeObject.description = this.recipeDescription;
    }

    public resetForm = () => {
        this.selectedIngredients = [];
        this.listOfIngredients = [];
        this.recipeName = "";
        this.recipeDescription = "";
        this.inputValue = "";
    }

    public addRecipe = async () => {
        try {
            await addRecipe(this.newRecipeObject);
            this.resetForm();
            return true;
        } catch (error) {
            popUpNotificationStore.handleError(error);
            return false;
        }
    }
}

export const addRecipePageStore = new AddRecipePageStore();
export const addRecipePageContext = createContext(addRecipePageStore);