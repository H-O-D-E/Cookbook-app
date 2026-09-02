using Cookbook_app.Models;

namespace Cookbook_app.Repositories;

public interface IRecipeBookRepository
{
    public Task<RecipeBook?> GetRecipeBookByIdAsync(int id);
    public Task AddRecipeBookAsync(RecipeBook recipeBook);
    public Task DeleteRecipeBookAsync(RecipeBook recipeBook);
    public Task UpdateRecipeBookAsync(RecipeBook recipeBook);
    
}