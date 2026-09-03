using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Models;
using Cookbook_app.Repositories;

namespace Cookbook_app.Services;

public class RecipeBookService : IRecipeBookService
{
    private readonly IRecipeBookRepository _recipeBookRepository;


    public RecipeBookService(IRecipeBookRepository recipeBookRepository)
    {
        _recipeBookRepository = recipeBookRepository;
    }

  

    public async Task<RecipeBook?> GetRecipeBookAsync(int recipeBookId, string userId)
    {
        var cookbook = await _recipeBookRepository.GetRecipeBookByIdAsync(recipeBookId);

        if (cookbook is null)
        {
            return null;
        }

        if (cookbook.UserId != userId) return null;

        return cookbook;


    }

    public async Task<RecipeBook> CreateRecipeBookAsync(
        CreateRecipeBookRequest request,
        string userId)
    {
        var recipeBook = new RecipeBook
        {
            Name = request.RecipeBookName,
            UserId = userId
        };

        await _recipeBookRepository.AddRecipeBookAsync(recipeBook);

        return recipeBook;
    }

    public async Task<RecipeBook?> UpdateRecipeBookAsync(
        int id,
        UpdateRecipeBookRequest request,
        string userId)
    {
        var recipeBook =
            await _recipeBookRepository.GetRecipeBookByIdAsync(id);

        if (recipeBook is null)
            return null;

        if (recipeBook.UserId != userId)
            return null;

        if (request.Name is not null)
            recipeBook.Name = request.Name;

        await _recipeBookRepository.UpdateRecipeBookAsync(recipeBook);

        return recipeBook;
    }

    public async Task<bool> DeleteRecipeBookAsync(int id, string userId)
    {

        var recipeBook = await _recipeBookRepository.GetRecipeBookByIdAsync(id);

        if (recipeBook is null) return false;

        if (recipeBook.UserId != userId) return false;

        await _recipeBookRepository.DeleteRecipeBookAsync(recipeBook);

        return true;



    }
}