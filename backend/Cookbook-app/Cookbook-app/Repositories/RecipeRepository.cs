using Cookbook_app.Data;
using Cookbook_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly CookbookDbContext _context;

    public RecipeRepository(CookbookDbContext context)
    {
        _context = context;
    }

    public async Task<Recipe?> GetRecipeByRecipeIdAsync(int id, string userId)
    {
        return await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id && r.RecipeBook.UserId==userId);
    }
    
    public async Task<List<Recipe>> GetRecipesByRecipeBookIdAsync(
        int recipeBookId,
        string userId)
    {
        return await _context.Recipes
            .Where(recipe =>
                recipe.RecipeBookId == recipeBookId &&
                recipe.RecipeBook.UserId == userId)
            .ToListAsync();
    }

 

    public async Task AddRecipeAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(Recipe recipe)
    {
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }
}