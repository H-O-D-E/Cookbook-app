namespace Cookbook_app.DTOs.ResponseDTO;

public record RecipeBookResponse(int RecipeBookId, string Name, string Description, string ImageUrl, float RecipeBookScore);