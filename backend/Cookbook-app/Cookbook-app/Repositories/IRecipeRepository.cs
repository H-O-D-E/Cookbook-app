using Cookbook_app.Models;

namespace Cookbook_app.Repositories;


public interface IRecipeRepository
{
    Task<Recipe?> GetRecipeByRecipeIdAsync(int recipeId, string userId);

    Task<List<Recipe>> GetRecipesByRecipeBookIdAsync(
        int recipeBookId,
        string userId);
    Task AddRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(Recipe recipe);
    
    //rate
    Task<Recipe?> RateRecipeAsync(int recipeId, string userId, int score);
    
    
    //explore page

    Task<List<Recipe>> GetOtherUsersRecipesAsync(string userId);

}