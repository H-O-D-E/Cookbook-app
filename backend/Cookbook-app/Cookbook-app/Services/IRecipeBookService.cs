using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Models;

namespace Cookbook_app.Services;

public interface IRecipeBookService
{
    Task<RecipeBook?> GetRecipeBookAsync(int recipeBookId, string userId);

    Task<RecipeBook> CreateRecipeBookAsync(
        CreateRecipeBookRequest request,
        string userId);

    Task<RecipeBook?> UpdateRecipeBookAsync(
        int id,
        UpdateRecipeBookRequest request,
        string userId);

    Task<bool> DeleteRecipeBookAsync(
        int id,
        string userId);
}