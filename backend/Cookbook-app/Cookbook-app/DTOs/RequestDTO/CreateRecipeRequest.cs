namespace Cookbook_app.DTOs.RequestDTO;

public record CreateRecipeRequest(string RecipeName, string Description, string Ingredients, string Instructions, int RecipebookId);