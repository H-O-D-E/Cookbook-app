using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Models;
using Cookbook_app.Repositories;

namespace Cookbook_app.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeBookRepository _recipeBookRepository;

    public RecipeService(IRecipeRepository recipeRepository, IRecipeBookRepository recipeBookRepository)
    {
        _recipeRepository = recipeRepository;
        _recipeBookRepository = recipeBookRepository;
    }
    
    public async Task<Recipe?> GetRecipeAsync(int recipeId, string userId)
    {
        var recipe = await _recipeRepository.GetRecipeByRecipeIdAsync(recipeId ,userId);
        
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

    public async Task<Recipe> CreateRecipeAsync(CreateRecipeRequest request, string userId)
    {        
        var recipeBook = await _recipeBookRepository.GetRecipeBookByIdAsync(// we need to check if the book actually blong tto the user before he can create a recipe Evan--
                                                                            
            request.RecipebookId,
            userId);

        if (recipeBook is null) return null;
        
            var newRecipe = new Recipe
            {
                Name = request.RecipeName, Description = request.Description, ImageUrl = request.ImageUrl, Ingredients = request.Ingredients,
                Instructions = request.Instructions, RecipeBookId = request.RecipebookId
            };
            await _recipeRepository.AddRecipeAsync(newRecipe);
            return newRecipe;
        }
    

    public async Task<Recipe?> UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request, string userId)
    {
        var existingRecipe = await GetRecipeAsync(recipeId, userId);
        
        if (existingRecipe is null) return null;

        if (request.Name is not null) existingRecipe.Name = request.Name;
        if (request.Description is not null) existingRecipe.Description = request.Description;
        if (request.ImageUrl is not null) existingRecipe.ImageUrl = request.ImageUrl;
        if (request.Ingredients is not null) existingRecipe.Ingredients = request.Ingredients;
        if (request.Instructions is not null) existingRecipe.Instructions = request.Instructions;
        
        await _recipeRepository.UpdateRecipeAsync(existingRecipe);
        return existingRecipe;
    }

    public async Task<bool> DeleteRecipeAsync(int recipeId, string userId)
    {
        var existingRecipe = await _recipeRepository.GetRecipeByRecipeIdAsync(recipeId, userId);

        if (existingRecipe is null) return false;

        await _recipeRepository.DeleteRecipeAsync(existingRecipe);

        return true;
    }
}