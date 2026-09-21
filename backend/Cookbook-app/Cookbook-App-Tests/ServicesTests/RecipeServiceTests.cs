using NUnit.Framework;
using Moq;
using Cookbook_app.Models;
using Cookbook_app.Services;
using Cookbook_app.Repositories;
using Cookbook_app.DTOs.RequestDTO;

namespace RecipeService_Tests;

public class RecipeServiceTests
{

    private Mock<IRecipeRepository> _recipeRepositoryMock = null!;
    private RecipeService _recipeService = null!;

    [SetUp]
    public void Setup()
    {
        _recipeRepositoryMock = new Mock<IRecipeRepository>();
        _recipeService = new RecipeService(_recipeRepositoryMock.Object);

    }

    [Test]
    // Checks that asking for a recipe returns the recipe from the repository
    public async Task GetRecipe_ReturnsRecipe()
    {
        var recipe = new Recipe { RecipeId = 1, Name = "Pizza" };
        _recipeRepositoryMock.Setup(r => r.GetRecipeByRecipeIdAsync(1)).ReturnsAsync(recipe);
        var result = await _recipeService.GetRecipeAsync(1);
        Assert.That(result, Is.SameAs(recipe));
    }

    [Test]
    // Checks that creating a recipe returns it and saves it in the repository
    public async Task CreateRecipe_ReturnsAndSavesNewRecipe()
    {

        var request = new CreateRecipeRequest("pizza",
            "italian",
            "tomato",
            "justcookit",
            3);

         var result = await _recipeService.CreateRecipeAsync(request);
        
        Assert.That(result.Name, Is.EqualTo(request.RecipeName));
        _recipeRepositoryMock.Verify(r => r.AddRecipeAsync(It.Is<Recipe>(recipe =>
            recipe.Name == request.RecipeName && recipe.RecipeBookId == request.RecipebookId)), Times.Once);


    }
    
    [Test]
    // Checks that updating a recipe changes its details and saves the changes
    public async Task UpdateRecipe_UpdatesAndReturnsExistingRecipe()
    {
        var recipe = new Recipe { RecipeId = 1, Name = "Old name" };
        var request = new UpdateRecipeRequest("New name", null, null, null);
        _recipeRepositoryMock.Setup(r => r.GetRecipeByRecipeIdAsync(recipe.RecipeId)).ReturnsAsync(recipe);

        var result = await _recipeService.UpdateRecipeAsync(recipe.RecipeId, request);

        Assert.That(result, Is.SameAs(recipe));
        Assert.That(recipe.Name, Is.EqualTo(request.Name));
        _recipeRepositoryMock.Verify(r => r.UpdateRecipeAsync(recipe), Times.Once);
    }

    [Test]
    // Checks that deleting an existing recipe removes it and returns true
    public async Task DeleteRecipe_DeletesExistingRecipeAndReturnsTrue()
    {
        var recipe = new Recipe { RecipeId = 1, Name = "Pizza" };
        _recipeRepositoryMock.Setup(r => r.GetRecipeByRecipeIdAsync(recipe.RecipeId)).ReturnsAsync(recipe);

        var result = await _recipeService.DeleteRecipeAsync(recipe.RecipeId);

        Assert.That(result, Is.True);
        _recipeRepositoryMock.Verify(r => r.DeleteRecipeAsync(recipe), Times.Once);
    }
}
