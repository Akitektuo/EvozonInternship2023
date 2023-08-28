namespace MealPlan.Data.Models.Recipes
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
    }
}