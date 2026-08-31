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
    
    
    public Task<RecipeBook> GetRecipeBookById(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddRecipeBook(RecipeBook recipeBook)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRecipeBook(RecipeBook recipeBook)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRecipeBook(RecipeBook recipeBook)
    {
        throw new NotImplementedException();
    }
}