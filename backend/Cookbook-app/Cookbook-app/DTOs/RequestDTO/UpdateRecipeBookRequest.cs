namespace Cookbook_app.DTOs.RequestDTO;

public record UpdateRecipeBookRequest(string? Name, string? Description, string? ImageUrl);