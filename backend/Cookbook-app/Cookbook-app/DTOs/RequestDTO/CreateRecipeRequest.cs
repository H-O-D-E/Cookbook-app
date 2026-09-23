namespace Cookbook_app.DTOs.RequestDTO;

public record CreateRecipeRequest(string RecipeName, string Description, string ImageUrl, string Ingredients, string Instructions);