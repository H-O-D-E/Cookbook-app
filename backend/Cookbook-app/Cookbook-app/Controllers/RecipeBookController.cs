using System.Security.Claims;
using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.DTOs.ResponseDTO;
using Cookbook_app.Models;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var book = await _service.GetRecipeBookAsync(id, UserId);
        if (book is null) return NotFound("Recipe book not found :( ");

        return Ok(new RecipeBookResponse(book.RecipeBookId, book.Name, book.Description, book.ImageUrl, book.RecipeBookScore,book.Tag));
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedResult<RecipeBookResponse>>> GetAllRecipeBooksAsync( [FromQuery] ListQuery options)
    {
        var books = await _service.GetAllRecipeBooksAsync(UserId, options);

        var  response= books.Items
            .Select(book => new RecipeBookResponse(
                book.RecipeBookId,
                book.Name,
                book.Description,
                book.ImageUrl,
                book.RecipeBookScore,
                book.Tag))
            .ToList();

        return Ok(new PagedResult<RecipeBook>(
              books.Items,
            books .TotalCount,
            books.Page,
            books.PageSize));
    }
    
    

    [HttpPost]
    [ActionName("GetRecipeBookAsync")]        // To avoid removal of Async suffix from action name
    public async Task<ActionResult<RecipeBookResponse>> CreateRecipeBookAsync(CreateRecipeBookRequest request)
    {
        var book = await _service.CreateRecipeBookAsync(request, UserId);
        var response = new RecipeBookResponse(book.RecipeBookId, book.Name, book.Description, book.ImageUrl, book.RecipeBookScore,book.Tag);
        
        return CreatedAtAction(nameof(GetRecipeBookAsync), new {id = book.RecipeBookId}, response);
    }
    

    [HttpPut("{id:int}")]
    public async Task<ActionResult<RecipeBookResponse>> UpdateRecipeBookAsync(int id, UpdateRecipeBookRequest request)
    {
        var book = await _service.UpdateRecipeBookAsync(id, request, UserId);

        if (book is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipebook not found",
                Detail = $"No recipebook with id {id} exists.",
                Status = StatusCodes.Status404NotFound
            });
        }
        return Ok(new RecipeBookResponse(book.RecipeBookId, book.Name, book.Description, book.ImageUrl, book.RecipeBookScore,book.Tag));
    }
    
    

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRecipeBookAsync(int id)
    {
        if (!await _service.DeleteRecipeBookAsync(id, UserId))
        {
            return NotFound(new ProblemDetails
            {
                Title = "Recipebook not found",
                Detail = $"No recipebook with id {id} found.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }
    
}