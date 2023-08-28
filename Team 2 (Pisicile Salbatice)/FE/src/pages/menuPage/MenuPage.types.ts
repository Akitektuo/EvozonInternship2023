import { CategoriesEnum } from "../../shared/CategoriesEnum";

export interface MenuType {
    id: number;
    category: CategoriesEnum;
    name: string;
    price: number;
    meals: MealType[];
}

export interface MealType {
    id: number;
    name: string;
    description: string;
    price: number;
    mealTypeId: MealEnum;
    ingredients: string[];
}

export enum MealEnum {
    Breakfast = 1,
    Lunch = 2,
    Dinner = 3,
    Dessert = 4,
    Snack = 5
}