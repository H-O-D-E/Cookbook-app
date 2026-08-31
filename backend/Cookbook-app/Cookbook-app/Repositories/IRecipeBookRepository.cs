using Cookbook_app.Models;

namespace Cookbook_app.Repositories;

public interface IRecipeBookRepository
{
    public Task<RecipeBook> GetRecipeBookById(int id);
    public Task AddRecipeBook(RecipeBook recipeBook);
    public Task DeleteRecipeBook(RecipeBook recipeBook);
    public Task UpdateRecipeBook(RecipeBook recipeBook);
    
}