using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Models;
using Cookbook_app.Repositories;

namespace Cookbook_app.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    
    public async Task<Recipe?> GetRecipeAsync(int recipeId)
    {
        var recipe = await _recipeRepository.GetRecipeByRecipeIdAsync(recipeId);
        
        if (recipe is null) return null;

        return recipe;
    }

    public async Task<List<Recipe>> GetRecipesByRecipeBookIdAsync(
        int recipeBookId,
        string userId)
    {
        return await _recipeRepository.GetRecipesByRecipeBookIdAsync(
            recipeBookId,
            userId);
    }

    public async Task<Recipe> CreateRecipeAsync(CreateRecipeRequest request)
    {
        var newRecipe = new Recipe { Name = request.RecipeName, Description = request.Description, ImageUrl = request.ImageUrl, Ingredients = request.Ingredients, 
            Instructions = request.Instructions, RecipeBookId = request.RecipebookId 
        };
        await _recipeRepository.AddRecipeAsync(newRecipe);
        return newRecipe;
    }

    public async Task<Recipe?> UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request)
    {
        var existingRecipe = await GetRecipeAsync(recipeId);
        
        if (existingRecipe is null) return null;

        if (request.Name is not null) existingRecipe.Name = request.Name;
        if (request.Description is not null) existingRecipe.Description = request.Description;
        if (request.ImageUrl is not null) existingRecipe.ImageUrl = request.ImageUrl;
        if (request.Ingredients is not null) existingRecipe.Ingredients = request.Ingredients;
        if (request.Instructions is not null) existingRecipe.Instructions = request.Instructions;
        
        await _recipeRepository.UpdateRecipeAsync(existingRecipe);
        return existingRecipe;
    }

    public async Task<bool> DeleteRecipeAsync(int recipeId)
    {
        var existingRecipe = await _recipeRepository.GetRecipeByRecipeIdAsync(recipeId);

        if (existingRecipe is null) return false;

        await _recipeRepository.DeleteRecipeAsync(existingRecipe);

        return true;
    }
}