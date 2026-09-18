namespace Cookbook_app.DTOs.ResponseDTO;

public record GetRecipeResponse(int RecipeId, string Name, string Description, string ImageUrl, string Ingredients, string Instructions, float RecipeScore);