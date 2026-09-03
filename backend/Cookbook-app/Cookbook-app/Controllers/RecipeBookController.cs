using System.Security.Claims;
using Cookbook_app.DTOs.ResponseDTO;
using Cookbook_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Controllers;

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

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RecipeBookResponse>> GetRecipeBookAsync(int id)
    {
        var book = await _service.GetRecipeBookAsync(id);
        if (book is null) return NotFound("Recipe book not found :( ");

        return Ok(new RecipeBookResponse(book.RecipeBookId, book.Name, book.RecipeBookScore));
    }

    [HttpPost]
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

        if (await _service.DeleteRecipeBookAsync(id, UserId) == null)
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