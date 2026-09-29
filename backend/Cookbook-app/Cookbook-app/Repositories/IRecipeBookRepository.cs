using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Models;


namespace Cookbook_app.Repositories;

public interface IRecipeBookRepository
{
    public Task<RecipeBook?> GetRecipeBookByIdAsync(int recipe, string id);

    public  Task<PagedResult<RecipeBook>> GetAllRecipeBooksAsync(string userId, ListQuery options);

    public Task<RecipeBook?> GetRecipeBookByNameAsync(string name, string userId);
    public Task AddRecipeBookAsync(RecipeBook recipeBook);
    public Task DeleteRecipeBookAsync(RecipeBook recipeBook);
    public Task UpdateRecipeBookAsync(RecipeBook recipeBook);
    
}