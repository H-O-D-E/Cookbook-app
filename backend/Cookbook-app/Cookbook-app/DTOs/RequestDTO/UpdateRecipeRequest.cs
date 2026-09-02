namespace Cookbook_app.DTOs.RequestDTO;

public record UpdateRecipeRequest(string? Name, string? Description, string? Ingredients, string? Instructions);