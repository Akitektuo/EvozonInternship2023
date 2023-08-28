using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlan.Migrations.Migrations
{
    public partial class AddWalletBalanceToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WalletBalance",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletBalance",
                table: "Users");
        }
    }
}
