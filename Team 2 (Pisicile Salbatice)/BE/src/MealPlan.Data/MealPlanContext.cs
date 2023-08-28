using MealPlan.Data.Configurations.ApplicationVersions;
using MealPlan.Data.Configurations.MealConfigurations;
using MealPlan.Data.Configurations.OrderConfigurations;
using MealPlan.Data.Configurations.RecipeConfigurations;
using MealPlan.Data.Configurations.UserConfigurations;
using MealPlan.Data.Models.ApplicationVersions;
using MealPlan.Data.Models.Meals;
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
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuTypeLookup> MenuLookup { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<MealTypeLookup> MealLookup { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<StatusLookup> StatusLookup { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationVersionConfiguration());
            builder.ApplyConfiguration(new UserConfigurations());
            builder.ApplyConfiguration(new RoleLookupConfigurations());

            builder.ApplyConfiguration(new RecipeConfigurations());
            builder.ApplyConfiguration(new IngredientConfigurations()); 
            builder.ApplyConfiguration(new RecipeIngredientConfigurations());

            builder.ApplyConfiguration(new MenuConfigurations());
            builder.ApplyConfiguration(new MenuTypeLookupConfigurations());

            builder.ApplyConfiguration(new MealConfigurations());
            builder.ApplyConfiguration(new MealTypeLookupConfigurations());

            builder.ApplyConfiguration(new OrderConfigurations());
            builder.ApplyConfiguration(new StatusLookupConfigurations());
        }
    }
}
