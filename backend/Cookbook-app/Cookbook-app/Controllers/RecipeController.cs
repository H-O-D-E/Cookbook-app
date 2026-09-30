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
        var recipe = await _recipeService.GetRecipeAsync(recipeId, UserId);
        
        if (recipe is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipe not found",
                Detail = $"No recipe with id {recipeId} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(new GetRecipeResponse(recipe.RecipeId, recipe.Name, recipe.Description, recipe.ImageUrl, recipe.Ingredients,
            recipe.Instructions, recipe.RecipeScore, recipe.IsPublic));
    }
    
    [HttpGet("/api/recipebooks/{recipeBookId:int}/recipes")]
    public async Task<ActionResult<IEnumerable<GetRecipeResponse>>>
        GetRecipesByRecipeBookIdAsync(int recipeBookId)
    {
        var recipes = await _recipeService
            .GetRecipesByRecipeBookIdAsync(recipeBookId, UserId);

        var response = recipes.Select(recipe =>
            new GetRecipeResponse(
                recipe.RecipeId,
                recipe.Name,
                recipe.Description,
                recipe.ImageUrl,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.RecipeScore, recipe.IsPublic
            ));

        return Ok(response);
    }

    [HttpPost("/api/recipebooks/{recipeBookId:int}/recipes")]
    public async Task<ActionResult<GetRecipeResponse>> CreateRecipeAsync(
        CreateRecipeRequest request,
        int recipeBookId)
    {
        var recipe = await _recipeService.CreateRecipeAsync(
            request,
            UserId,
            recipeBookId
            
        );

        if (recipe is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipe book not found",
                Detail = $"Recipe book with id {recipeBookId} was not found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        var response = new GetRecipeResponse(
            recipe.RecipeId,
            recipe.Name,
            recipe.Description,
            recipe.ImageUrl,
            recipe.Ingredients,
            recipe.Instructions,
            recipe.RecipeScore,
            recipe.IsPublic
        );

        return CreatedAtAction(
            "GetRecipe",
            new { recipeId = recipe.RecipeId },
            response
        );
    }

    [HttpPut("{recipeId:int}")]
    public async Task<ActionResult<GetRecipeResponse>> UpdateRecipe(int recipeId, UpdateRecipeRequest request)
    {
        var recipe = await _recipeService.UpdateRecipeAsync(recipeId, request, UserId);

        if (recipe is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipe not found",
                Detail = $"No recipe with id {recipeId} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(new GetRecipeResponse(recipe.RecipeId, recipe.Name, recipe.Description, recipe.ImageUrl, recipe.Ingredients,
            recipe.Instructions, recipe.RecipeScore, recipe.IsPublic));
    }

    [HttpDelete("{recipeId:int}")]
    public async Task<IActionResult> DeleteRecipe(int recipeId)
    {
        if (!await _recipeService.DeleteRecipeAsync(recipeId, UserId))
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
    
    
    [HttpGet("explore")]
    public async Task<ActionResult<PagedResult<GetRecipeResponse>>>
        GetOtherUsersRecipesAsync([FromQuery] ListQuery options)
    {
        var recipes = await _recipeService.GetOtherUsersRecipesAsync(UserId, options);

        var response = recipes.Items.Select(recipe => new GetRecipeResponse(
            recipe.RecipeId,
            recipe.Name,
            recipe.Description,
            recipe.ImageUrl,
            recipe.Ingredients,
            recipe.Instructions,
            recipe.RecipeScore,
            recipe.IsPublic
        )).ToList();

        return Ok(new PagedResult<GetRecipeResponse>(
            response,
            recipes.TotalCount,
            recipes.Page,
            recipes.PageSize));
    }
    
    [HttpPut("{recipeId:int}/rating")]
    public async Task<ActionResult<GetRecipeResponse>> RateRecipeAsync(
        int recipeId, RateRecipeRequest request)
    {
        try
        {
            var recipe = await _recipeService.RateRecipeAsync(
                recipeId, UserId, request.Score);

            if (recipe is null)
                return NotFound();

            return Ok(new GetRecipeResponse(
                recipe.RecipeId,
                recipe.Name,
                recipe.Description,
                recipe.ImageUrl,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.RecipeScore,
                recipe.IsPublic
            ));
        }
        catch (InvalidOperationException ex)
            when (ex.Message == "You cannot rate your own recipe.")
        {
            return Forbid();
        }
    }
    
    
}
