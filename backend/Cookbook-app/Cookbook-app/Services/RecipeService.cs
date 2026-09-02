using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Repositories;

namespace Cookbook_app.Services;

public class RecipeService : IRecipeService
{
    private readonly RecipeRepository _recipeRepository;

    public RecipeService(RecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    
    public async Task<Recipe?> GetRecipeAsync(int recipeId)
    {
        return await _recipeRepository.GetRecipeByRecipeIdAsync(recipeId);
    }

    public async Task CreateRecipeAsync(string recipeName, string desc, string ingredients, string instructions, int recipebookId)
    {
        var existingRecipe = await _recipeRepository.GetRecipeByNameAsync(recipeName);
        if (existingRecipe != null)
        {
            throw new Exception("Recipe already exists");
        }

        var newRecipe = new Recipe { Name = recipeName, Description = desc, Ingredients = ingredients, 
            Instructions = instructions, RecipeScore = 0, RecipeBookId = recipebookId 
        };
        await _recipeRepository.AddRecipeAsync(newRecipe);
    }

    public async Task UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request)
    {
        var existingRecipe = await GetRecipeAsync(recipeId);
        if (existingRecipe == null)
        {
            throw new Exception("Recipe does not exists");
        }

        if (request.Name != null) existingRecipe.Name = request.Name;
        if (request.Description != null) existingRecipe.Description = request.Description;
        if (request.Ingredients != null) existingRecipe.Ingredients = request.Ingredients;
        if (request.Instructions != null) existingRecipe.Instructions = request.Instructions;
        
        await _recipeRepository.UpdateRecipeAsync(existingRecipe);
    }

    public async Task DeleteRecipeAsync(int recipeId)
    {
        var existingRecipe = await GetRecipeAsync(recipeId);
        if (existingRecipe == null)
        {
            throw new Exception("Recipe does not exists");
        }

        await _recipeRepository.DeleteRecipeAsync(existingRecipe);
    }
}