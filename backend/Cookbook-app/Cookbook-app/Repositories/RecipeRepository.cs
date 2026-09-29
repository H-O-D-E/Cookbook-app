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

    public async Task<Recipe?> RateRecipeAsync(
        int recipeId, string userId, int score)
    {
        if (score < 1 || score > 5)
            throw new ArgumentOutOfRangeException(nameof(score));

        var recipe = await _context.Recipes
            .Include(r => r.RecipeBook)
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        if (recipe is null)
            return null;

        if (recipe.RecipeBook.UserId == userId)
            throw new InvalidOperationException(
                "You cannot rate your own recipe.");

        var rating = recipe.Ratings
            .SingleOrDefault(r => r.UserId == userId);

        if (rating is null)
        {
            recipe.Ratings.Add(new RecipeRating
            {
                RecipeId = recipeId,
                UserId = userId,
                Score = score
            });
        }
        else
        {
            rating.Score = score;
        }

        recipe.RecipeScore =
            (float)recipe.Ratings.Average(r => r.Score);

        await _context.SaveChangesAsync();

        return recipe;
    }

    public async Task<List<Recipe>> GetOtherUsersRecipesAsync(string userId)
    {
        return await _context.Recipes
            .AsNoTracking()
            .Where(recipe => recipe.RecipeBook.UserId != userId) //everyones else
            .OrderBy(recipe => recipe.RecipeId)
            .ToListAsync();
    }
    
    
    
}