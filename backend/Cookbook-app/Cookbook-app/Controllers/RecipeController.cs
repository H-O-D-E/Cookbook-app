using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.DTOs.ResponseDTO;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_app.Controllers;

// REVIEW(sec): this entire controller has no [Authorize]. Every recipe endpoint (create, update, delete included) is open to anonymous callers. RecipeBookController gets this right two files over; copy it here.
[ApiController]
[Route("/api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    // REVIEW(api): there is no endpoint to list recipes, and GetAllRecipesAsync exists on the repository but nothing calls it. Dead code on one side, a missing endpoint on the other.
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

    // REVIEW(sec): this is the textbook version of the problem to look for. CreateRecipeRequest carries RecipebookId, so the client picks which recipe book the new recipe lands in, and nothing checks that the book belongs to the caller. Any user can write into any other user's cookbook by incrementing an integer. The fix is the pattern already used next door: take the user id from the token claims and have the service verify the target book belongs to that user before touching it. Better still, make it POST /api/recipebooks/{bookId}/recipes so the ownership check has an obvious home.
    [HttpPost]
    public async Task<ActionResult<GetRecipeResponse>> CreateRecipeAsync(CreateRecipeRequest request)
    {
        var recipe = await _recipeService.CreateRecipeAsync(request);

        // REVIEW(bug): CreatedAtAction("GetRecipe", ...) works only because ASP.NET Core strips the Async suffix from action names by default. RecipeBookController fights that same behaviour with an [ActionName] attribute. Pick one convention for the whole project, and use nameof() rather than a string literal so a rename cannot silently break the Location header.
        return CreatedAtAction(
            "GetRecipe",
            new { recipeId = recipe.RecipeId },
            new GetRecipeResponse(recipe.Name, recipe.Description, recipe.Ingredients,
                recipe.Instructions, recipe.RecipeScore));
    }

    // REVIEW(sec): same problem. The caller passes any recipe id and edits it. There is no ownership check anywhere in the update path.
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

    // REVIEW(sec): and the same for delete. Anyone, not even logged in, can delete any recipe in the system by id.
    [HttpDelete("{recipeId:int}")]
    public async Task<ActionResult<bool>> DeleteRecipe(int recipeId)
    {
        var deleted = await _recipeService.DeleteRecipeAsync(recipeId);

        // REVIEW(bug): if (false). The result of DeleteRecipeAsync above is assigned to 'deleted' and then never used, so this block is unreachable and a delete of a non-existent recipe returns 204 instead of 404. This is what a compiler warning is trying to tell you; turn warnings into errors in the csproj and this cannot happen.
        if (false)
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