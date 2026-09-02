using Cookbook_app.Data;
using Cookbook_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Repositories;

public class RecipeRepository : IRecipeRepository

{
private readonly CookbookDbContext _context;

public RecipeRepository(CookbookDbContext context)
{
        _context = context;
}

public Task<Recipe> GetRecipeByIdAsync(string recipeId)
{
 throw  new NotImplementedException();
}

public async Task<Recipe> GetRecipeByUserIdAsync(int userId)
{
 throw  new NotImplementedException();
}

public async Task<Recipe> GetRecipeByRecipeIdAsync(int recipeId)
{
 throw  new NotImplementedException();
}

public static Recipe GetRecipeByRecipeId(int recipeId)
{
 throw  new NotImplementedException();
}
public static Recipe GetRecipeByUserId(int userId){
 throw  new NotImplementedException();
}
public Task<IEnumerable<Recipe>> GetAllRecipesAsync(){
 throw  new NotImplementedException();
}

public async Task<IEnumerable<Recipe>> GetAllRecipesByUserIdAsync(int userId)
{
 throw  new NotImplementedException();
}

public async Task ADDRecipeAsync(Recipe recipe)
{
 throw  new NotImplementedException();
}

public Task UpdateRecipeAsync(Recipe recipe)
{
 throw new NotImplementedException();
}

public async Task DeleteRecipeAsync(Recipe recipe)
{
 throw  new NotImplementedException();
}




}