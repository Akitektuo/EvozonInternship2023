export interface UserCredentials {
    email: string;
    password: string;
}

export interface UserDetails {
    email: string;
    firstName: string;
    lastName: string;
    role: string;
    token: string;
}

export const EmptyUserCredentials: UserCredentials = {
    email: "",
    password: ""
}

export interface UserInformation {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export const EmptyUserInformation: UserInformation = {
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
}

export interface PaginationInformation {
    pageNumber: number;
    pageSize: number;
}

export const EmptyPaginationInformation: PaginationInformation = {
    pageNumber: 1,
    pageSize: 10
}

export interface Recipe {
    id: number;
    name: string;
    description: string;
    ingredients: string[];
}
export interface RecipeDetails {
    description: string;
    ingredients: string[];
}

export enum MenuCategory {
    Fitness = 1,
    FoodLover = 2,
    Gym = 3,
    Vegetarian = 4
}

export enum MealType {
    Breakfast = 1,
    Lunch = 2,
    Dinner = 3,
    Dessert = 4,
    Snack = 5
}

export interface Menu {
    id: number;
    description: string;
    name: string;
    price: number;
}

export interface MealDetails {
    name: string;
    mealTypeId: number;
    recipeDetails: RecipeDetails;
}

export interface MenuDetails {
    name: string;
    description: string;
    price: number;
    mealsDetails: MealDetails[];
}

export interface CategoryInformation {
    pageNumber: number;
    pageSize: number;
    categoryId: number;
}

export interface RecipeRecords {
    items: Recipe[];
    totalRecords: number;
}

export interface Meal {
    mealName: string;
    mealType: string;
    listOfIngredients: [];
}

export interface Menu {
    menuName: string;
    menuDescription: string;
    price:  number;
    listOfMeals: Meal[];
    recipeDescription: string;
}

export interface Ingredient {
    id: number;
    name: string;
}

export interface RecipeObject {
    description: string;
    name: string;
    ingredientIds: number[];
}

export const EmptyNewRecipeObject: RecipeObject = {
    description: "",
    name: "",
    ingredientIds: [],
}

export interface SuggestedRecipe {
    description:string;
    name:string;
    ingredients: string[];
}

export interface SuggestedMeal {
    name:string;
    price:number;
    mealTypeId:number;
    recipe: SuggestedRecipe;
}

export interface SuggestedMenu {
    name:string;
    description:string;
    categoryId:number;
    totalPrice:number;
    meals: SuggestedMeal[];
}

export interface CategoryAndPrice {
    categoryId: number;
    priceSuggestion: number;
}

export const emptyCategoryAndPrice: CategoryAndPrice = {
    categoryId: MenuCategory.Fitness,
    priceSuggestion: 0
}