using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.DTOs.ResponseDTO;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Cookbook_app.Controllers;

[ApiController]

[Route("/api/recipes")]
[Authorize]


public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private string UserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new UnauthorizedAccessException("User ID claim is missing.");

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet("{recipeId:int}")]
    public async Task<ActionResult<GetRecipeResponse>> GetRecipeAsync(int recipeId)
    {
        var recipe = await _recipeService.GetRecipeAsync(recipeId);
        
        if (recipe is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipe not found",
                Detail = $"No recipe with id {recipeId} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(new GetRecipeResponse(recipe.Name, recipe.Description, recipe.Ingredients,
            recipe.Instructions, recipe.RecipeScore));
    }
    
    [HttpGet("/api/recipebooks/{recipeBookId:int}/recipes")]
    public async Task<ActionResult<IEnumerable<GetRecipeResponse>>>
        GetRecipesByRecipeBookIdAsync(int recipeBookId)
    {
        var recipes = await _recipeService
            .GetRecipesByRecipeBookIdAsync(recipeBookId, UserId);

        var response = recipes.Select(recipe =>
            new GetRecipeResponse(
                recipe.Name,
                recipe.Description,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.RecipeScore
            ));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<GetRecipeResponse>> CreateRecipeAsync(CreateRecipeRequest request)
    {
        var recipe = await _recipeService.CreateRecipeAsync(request);

        return CreatedAtAction(
            "GetRecipe",
            new { recipeId = recipe.RecipeId },
            new GetRecipeResponse(recipe.Name, recipe.Description, recipe.Ingredients,
                recipe.Instructions, recipe.RecipeScore));
    }

    [HttpPut("{recipeId:int}")]
    public async Task<ActionResult<GetRecipeResponse>> UpdateRecipe(int recipeId, UpdateRecipeRequest request)
    {
        var recipe = await _recipeService.UpdateRecipeAsync(recipeId, request);

        if (recipe is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipe not found",
                Detail = $"No recipe with id {recipeId} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(new GetRecipeResponse(recipe.Name, recipe.Description, recipe.Ingredients,
            recipe.Instructions, recipe.RecipeScore));
    }

    [HttpDelete("{recipeId:int}")]
    public async Task<ActionResult<bool>> DeleteRecipe(int recipeId)
    {
        var deleted = await _recipeService.DeleteRecipeAsync(recipeId);

        if (deleted)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipe not found",
                Detail = $"No recipe with id {recipeId} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }
}