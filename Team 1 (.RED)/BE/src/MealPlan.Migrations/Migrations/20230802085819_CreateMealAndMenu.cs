using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlan.Migrations.Migrations
{
    public partial class CreateMealAndMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealTypeLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypeLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_CategoryLookup_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MealTypeId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_MealTypeLookup_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealTypeLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoryLookup",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fitness" },
                    { 2, "FoodLover" },
                    { 3, "Gym" },
                    { 4, "Vegetarian" }
                });

            migrationBuilder.InsertData(
                table: "MealTypeLookup",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Breakfast" },
                    { 2, "Lunch" },
                    { 3, "Dinner" },
                    { 4, "Dessert" },
                    { 5, "Snack" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealTypeId",
                table: "Meals",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MenuId",
                table: "Meals",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_RecipeId",
                table: "Meals",
                column: "RecipeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_CategoryId",
                table: "Menus",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MealTypeLookup");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "CategoryLookup");
        }
    }
}
