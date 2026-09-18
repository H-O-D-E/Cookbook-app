namespace Cookbook_app.DTOs.RequestDTO;

public record UpdateRecipeRequest(string? Name, string? Description, string? ImageUrl, string? Ingredients, string? Instructions);