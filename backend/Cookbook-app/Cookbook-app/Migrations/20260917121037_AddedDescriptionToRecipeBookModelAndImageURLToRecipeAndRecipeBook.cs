using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_app.Migrations
{
    /// <inheritdoc />
    public partial class AddedDescriptionToRecipeBookModelAndImageURLToRecipeAndRecipeBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RecipeBooks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "RecipeBooks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RecipeBooks");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "RecipeBooks");
        }
    }
}
