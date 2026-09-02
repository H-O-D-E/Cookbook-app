using Cookbook_app.Models;

namespace Cookbook_app.Repositories;


public interface IRecipeRepository
{
    Task<Recipe?> GetRecipeByRecipeIdAsync(int recipeId);
    Task<IEnumerable<Recipe?>> GetAllRecipesAsync();
    Task AddRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(Recipe recipe);
}