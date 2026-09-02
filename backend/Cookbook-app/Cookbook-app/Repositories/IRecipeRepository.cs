using Cookbook_app.Models;

namespace Cookbook_app.Repositories;


public interface IRecipeRepository
{
    Task<Recipe> GetRecipeByIdAsync(string recipeId);
    Task<IEnumerable<Recipe>> GetAllRecipesAsync();
    Task<Recipe> GetRecipeByUserIdAsync(int userId);
    Task<IEnumerable<Recipe>> GetAllRecipesByUserIdAsync(int userId);
    Task AddRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
  Task DeleteRecipeAsync(Recipe recipe);
    
    
}