using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Repositories;

namespace Cookbook_app.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    
    // REVIEW(noob): 'var x = await ...; if (x is null) return null; return x;' is just 'return await ...'. Harmless, but the extra lines hide the fact that this method adds nothing over the repository.
    public async Task<Recipe?> GetRecipeAsync(int recipeId)
    {
        var recipe = await _recipeRepository.GetRecipeByRecipeIdAsync(recipeId);
        
        if (recipe is null) return null;

        return recipe;
    }

    // REVIEW(sec): this service has no notion of who is calling, so it cannot enforce anything. Compare it to RecipeBookService, where every method takes a userId. Thread the user through here too, and verify RecipebookId belongs to them before creating.
    // REVIEW(noob): no check that RecipebookId refers to an existing book either. If it does not, the insert fails on the foreign key and the client gets a 500 rather than a 400.
    public async Task<Recipe> CreateRecipeAsync(CreateRecipeRequest request)
    {
        var newRecipe = new Recipe { Name = request.RecipeName, Description = request.Description, Ingredients = request.Ingredients, 
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