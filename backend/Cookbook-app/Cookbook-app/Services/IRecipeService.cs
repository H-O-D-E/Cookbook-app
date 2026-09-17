using Cookbook_app.DTOs.RequestDTO;

namespace Cookbook_app.Services;

public interface IRecipeService
{
    Task<Recipe?> GetRecipeAsync(int recipeId, string userId);
    Task<List<Recipe>> GetRecipesByRecipeBookIdAsync(
        int recipeBookId,
        string userId);
    Task<Recipe> CreateRecipeAsync(CreateRecipeRequest request, string userId);
    Task<Recipe?> UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request, string userId);
    Task<bool> DeleteRecipeAsync(int recipeId, string userId);
}