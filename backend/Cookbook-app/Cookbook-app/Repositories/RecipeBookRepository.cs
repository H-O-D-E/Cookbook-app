using Cookbook_app.Data;
using Cookbook_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Repositories;

public class RecipeBookRepository : IRecipeBookRepository
{
    
    private readonly CookbookDbContext _context;

    public RecipeBookRepository(CookbookDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<RecipeBook?> GetRecipeBookByIdAsync(int id)
    {
        return await _context.RecipeBooks.FirstOrDefaultAsync(b => b.RecipeBookId == id);
    }

  

    public async Task AddRecipeBookAsync(RecipeBook recipeBook)
    {
        _context.RecipeBooks.Add(recipeBook);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeBookAsync(RecipeBook recipeBook)
    {
        _context.RecipeBooks.Remove(recipeBook);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeBookAsync(RecipeBook recipeBook)
    {
        _context.RecipeBooks.Update(recipeBook);
        await _context.SaveChangesAsync();
    }
}