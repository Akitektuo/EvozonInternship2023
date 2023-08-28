export interface RecipeListType {
    recipesList: RecipeType[];
    totalRecipesCount: number;
}

export interface RecipeType {
    id: number;
    name: string;
    description: string;
}