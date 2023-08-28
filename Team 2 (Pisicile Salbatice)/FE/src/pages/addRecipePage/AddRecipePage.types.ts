export interface IngredientType {
    id: number;
    name: string;
}

export interface RecipeType {
    name : string;
    description : string;
    ingredientIds : number[];
}

export const EMPTY_RECIPE: RecipeType = {
    name: "",
    description: "",
    ingredientIds: []
};

export interface IsTouchedType {
    nameIsTouched: boolean;
    descriptionIsTouched: boolean;
    ingredientIdsIsTouched: boolean;
}

export const EMPTY_IS_TOUCHED: IsTouchedType = {
    nameIsTouched: false,
    descriptionIsTouched: false,
    ingredientIdsIsTouched: false
};

export const ERROR_MESSAGE: string = "This field is required!";