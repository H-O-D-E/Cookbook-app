using Cookbook_app.Data;
using Cookbook_app.Models;

namespace Cookbook_app.Repositories;

public class RecipeBookRepository : IRecipeBookRepository
{
    
    private readonly CookbookDbContext _context;

    public RecipeBookRepository(CookbookDbContext context)
    {
        _context = context;
    }


    public Task<RecipeBook?> GetRecipeBookByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddRecipeBookAsync(RecipeBook recipeBook)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRecipeBookAsync(RecipeBook recipeBook)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRecipeBookAsync(RecipeBook recipeBook)
    {
        throw new NotImplementedException();
    }
}