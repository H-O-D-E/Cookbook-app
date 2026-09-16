using System.Security.Claims;
using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.DTOs.ResponseDTO;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_app.Controllers;

// REVIEW(good): [Authorize] on the controller plus the user id taken from the token claims, and every service call carries that id so the repository can filter by owner. This is exactly the right pattern, and it is what RecipeController is missing.
[ApiController]
[Route("api/recipebooks")]
[Authorize]
public class RecipeBookController : ControllerBase
{
    private readonly IRecipeBookService _service;

    public RecipeBookController(IRecipeBookService service)
    {
        _service = service;
    }

    // REVIEW(noob): FindFirstValue returns string?, and the property is declared string. With <Nullable>enable</Nullable> in the csproj this is a warning you are ignoring. It cannot actually be null under [Authorize], but say so in code (a null check that throws) rather than leaving it to luck.
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    // REVIEW(api): there is no endpoint to list the caller's recipe books, so a client that has just logged in cannot discover any ids. GET /api/recipebooks returning the user's own books is the missing piece.
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RecipeBookResponse>> GetRecipeBookAsync(int id)
    {
        var book = await _service.GetRecipeBookAsync(id, UserId);
        if (book is null) return NotFound("Recipe book not found :( ");

        return Ok(new RecipeBookResponse(book.RecipeBookId, book.Name, book.RecipeBookScore));
    }

    // REVIEW(bug): CreateRecipeBookAsync in the service returns null when a book with that name already exists (see RecipeBookService line 45), and this method dereferences the result immediately. A duplicate name gives the client a 500 from a NullReferenceException instead of a 409. Either throw a typed exception in the service or return a result object the controller can branch on.
    [HttpPost]
    // REVIEW(bug): this [ActionName] sits on CreateRecipeBookAsync, the POST action, not on the GET. So the POST is renamed to GetRecipeBookAsync, and the nameof() on line 40 resolves to the POST action, which means the Location header on a 201 points back at the create endpoint rather than the resource. Move the attribute onto GetRecipeBookAsync, which is what the comment says it was meant for.
    [ActionName("GetRecipeBookAsync")]        // To avoid removal of Async suffix from action name
    public async Task<ActionResult<RecipeBookResponse>> CreateRecipeBookAsync(CreateRecipeBookRequest request)
    {
        var book = await _service.CreateRecipeBookAsync(request, UserId);
        var response = new RecipeBookResponse(book.RecipeBookId, book.Name, book.RecipeBookScore);
        
        return CreatedAtAction(nameof(GetRecipeBookAsync), new {id = book.RecipeBookId}, response);
    }
    

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateRecipeBookAsync(int id, UpdateRecipeBookRequest request)
    {
        if (await _service.UpdateRecipeBookAsync(id, request, UserId) == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipebook not found",
                Detail = $"No recipebook with id {id} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRecipeBookAsync(int id)
    {
        if (!await _service.DeleteRecipeBookAsync(id, UserId))
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipebook not found",
                Detail = $"No recipebook with id {id} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }
    
}