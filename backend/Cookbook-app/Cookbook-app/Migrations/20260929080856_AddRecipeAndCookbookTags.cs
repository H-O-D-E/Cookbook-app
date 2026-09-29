using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_app.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeAndCookbookTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Recipes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "RecipeBooks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "RecipeBooks");
        }
    }
}
