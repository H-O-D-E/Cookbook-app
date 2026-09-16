using Cookbook_app.DTOs.RequestDTO;

namespace Cookbook_app.Services;

public interface IRecipeService
{
    Task<Recipe?> GetRecipeAsync(int recipeId);
    Task<List<Recipe>> GetRecipesByRecipeBookIdAsync(
        int recipeBookId,
        string userId);
    Task<Recipe> CreateRecipeAsync(CreateRecipeRequest request);
    Task<Recipe?> UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request);
    Task<bool> DeleteRecipeAsync(int recipeId);
}