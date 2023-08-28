using MealPlan.Data.Configurations.ApplicationVersions;
using MealPlan.Data.Configurations.Meals;
using MealPlan.Data.Configurations.Menus;
using MealPlan.Data.Configurations.Orders;
using MealPlan.Data.Configurations.Recipes;
using MealPlan.Data.Configurations.Users;
using MealPlan.Data.Models.ApplicationVersions;
using MealPlan.Data.Models.Meals;
using MealPlan.Data.Models.Menus;
using MealPlan.Data.Models.Orders;
using MealPlan.Data.Models.Recipes;
using MealPlan.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace MealPlan.Data
{
    public class MealPlanContext : DbContext
    {
        public MealPlanContext() { }

        public MealPlanContext(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<ApplicationVersion> ApplicationVersions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RoleLookup> RoleLookup { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<MealTypeLookup> MealTypeLookup { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuCategoryLookup> CategoryLookup { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatusLookup> OrderStatusLookup { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationVersionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleLookupConfiguration());
            builder.ApplyConfiguration(new IngredientConfiguration());
            builder.ApplyConfiguration(new RecipeConfiguration());
            builder.ApplyConfiguration(new RecipeIngredientConfiguration());
            builder.ApplyConfiguration(new MealConfiguration());
            builder.ApplyConfiguration(new MealTypeLookupConfiguration());
            builder.ApplyConfiguration(new MenuConfiguration());
            builder.ApplyConfiguration(new MenuCategoryLookupConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderStatusLookupConfiguration());
        }
    }
}