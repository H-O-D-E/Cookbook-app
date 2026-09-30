namespace Cookbook_app.DTOs.RequestDTO;


public record UpdateRecipeRequest(string? Name, string? Description, string? ImageUrl, string? Ingredients, string? Instructions, string? Tag,bool? IsPublic= null);
