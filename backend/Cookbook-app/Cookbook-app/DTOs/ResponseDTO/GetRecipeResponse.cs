namespace Cookbook_app.DTOs.ResponseDTO;

public record GetRecipeResponse(string Name, string Description, string Ingredients, string Instructions, float RecipeScore);