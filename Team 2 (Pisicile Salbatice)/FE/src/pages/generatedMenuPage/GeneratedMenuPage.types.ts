import { CategoriesEnum as CategoryEnum } from "../../shared/CategoriesEnum"
import { MealEnum } from "../menuPage/MenuPage.types"

export interface GeneratedMenuType {
    name: string;
    menuTypeId: CategoryEnum;
    meals: MealType[];
}

export interface MealType {
    name: string;
    description: string;
    price: number;
    mealTypeId: MealEnum;
    recipe: RecipeType;
}

export interface RecipeType {
    name: string;
    description: string;
    ingredients: string[];
}