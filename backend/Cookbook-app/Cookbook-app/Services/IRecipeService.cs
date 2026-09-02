using Cookbook_app.DTOs.RequestDTO;

namespace Cookbook_app.Services;

public interface IRecipeService
{
    Task<Recipe?> GetRecipeAsync(int recipeId);
    Task CreateRecipeAsync(string recipeName, string desc, string ingredients, string instructions, int recipebookId);
    Task UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request);
    Task DeleteRecipeAsync(int recipeId);
}