using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlan.Migrations.Migrations
{
    public partial class AddUniqueConstraintOnRecipeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Name",
                table: "Recipes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_Name",
                table: "Recipes");
        }
    }
}
