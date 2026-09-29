using Cookbook_app.Data;
using Cookbook_app.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cookbook_app.DTOs.RequestDTO;

namespace Cookbook_app.Repositories;

public class RecipeBookRepository : IRecipeBookRepository
{
    
    private readonly CookbookDbContext _context;

    public RecipeBookRepository(CookbookDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<RecipeBook?> GetRecipeBookByIdAsync(int id, string userid)
    {
        return await _context.RecipeBooks.FirstOrDefaultAsync(b => b.RecipeBookId == id && b.UserId == userid);
    }

    public async Task<PagedResult<RecipeBook>> GetAllRecipeBooksAsync(string userId, ListQuery options )
    {
        var results = _context.RecipeBooks
            .Include(recipebook => recipebook.Recipes)
            .AsNoTracking()
            .Where(book => book.UserId == userId);
        if (!string.IsNullOrWhiteSpace(options.Tag))
        {
            results = results.Where((book => book.Tag == options.Tag));
            
        }
        var totalCount= await results.CountAsync();
        var sorted = options.Sort == "desc"
            ? results.OrderByDescending(book => book.Name)
                .ThenBy(book => book.RecipeBookId)
            : results.OrderBy(book => book.Name)
                .ThenBy(book => book.RecipeBookId);

        var items = await sorted
            .Skip((options.Page - 1) * options.PageSize)
            .Take(options.PageSize);
            .ToListAsync();

        return new PagedResult<RecipeBook>(
            items, totalCount, options.Page, options.PageSize);
    }

    public async Task<RecipeBook?> GetRecipeBookByNameAsync(string name, string userid)
    {
        return await _context.RecipeBooks
            .Include(recipebook => recipebook.Recipes)
            .FirstOrDefaultAsync(b => b.Name == name && b.UserId == userid);
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