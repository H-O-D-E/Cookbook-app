// REVIEW(good): everything here goes through EF Core LINQ, so there is no string-concatenated SQL and no injection surface. If you ever reach for FromSqlRaw, use FromSqlInterpolated or pass parameters; never build the query with string interpolation yourself.
﻿using Cookbook_app.Data;
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

    public async Task<Recipe?> GetRecipeByRecipeIdAsync(int id)
    {
        return await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == id);
    }

    public async Task<IEnumerable<Recipe?>> GetAllRecipesAsync()
    {
        return await _context.Recipes.ToListAsync();
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