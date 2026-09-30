using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_app.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeVisibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Recipes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(
                """UPDATE "Recipes" SET "IsPublic" = TRUE;""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Recipes");
        }
    }
}
