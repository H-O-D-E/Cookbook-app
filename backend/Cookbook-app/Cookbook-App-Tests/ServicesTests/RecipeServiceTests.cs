using NUnit.Framework;
using Moq;
using Cookbook_app.Models;
using Cookbook_app.Services;
using Cookbook_app.Repositories;
using Cookbook_app.DTOs.RequestDTO;

namespace RecipeService_Tests;

public class RecipeServiceTests
{
    private const string UserId = "user 1";
    private Mock<IRecipeRepository> _recipeRepositoryMock = null!;
    private Mock<IRecipeBookRepository> _recipeBookRepositoryMock = null!;
    private RecipeService _recipeService = null!;

    [SetUp]
    public void Setup()
    {
        _recipeRepositoryMock = new Mock<IRecipeRepository>();
        _recipeBookRepositoryMock = new Mock<IRecipeBookRepository>();

        _recipeService = new RecipeService(
            _recipeRepositoryMock.Object,
            _recipeBookRepositoryMock.Object);
    }

    [Test]
    // Checks that asking for a recipe returns the recipe from the repository
    public async Task GetRecipe_ReturnsRecipe()
    {
        var recipe = new Recipe { RecipeId = 1, Name = "Pizza" };
        _recipeRepositoryMock
            .Setup(r => r.GetRecipeByRecipeIdAsync(1, UserId))
            .ReturnsAsync(recipe);

        var result = await _recipeService.GetRecipeAsync(1, UserId);

        Assert.That(result, Is.SameAs(recipe));
    }

    [Test]
    // Checks that creating a recipe returns it and saves it in the repository
    public async Task CreateRecipe_ReturnsAndSavesNewRecipe()
    {

        var request = new CreateRecipeRequest(
            "pizza",
            "italian",
            "https://example.com/pizza.jpg",
            "tomato",
            "just cook it",
            3);
        var recipeBook = new RecipeBook
        {
            RecipeBookId = request.RecipebookId,
            UserId = UserId
        };
        _recipeBookRepositoryMock
            .Setup(r => r.GetRecipeBookByIdAsync(request.RecipebookId, UserId))
            .ReturnsAsync(recipeBook);

        var result = await _recipeService.CreateRecipeAsync(request, UserId);
        
        Assert.That(result.Name, Is.EqualTo(request.RecipeName));
        _recipeRepositoryMock.Verify(r => r.AddRecipeAsync(It.Is<Recipe>(recipe =>
            recipe.Name == request.RecipeName && recipe.RecipeBookId == request.RecipebookId)), Times.Once);


    }
    
    [Test]
    // Checks that updating a recipe changes its details and saves the changes
    public async Task UpdateRecipe_UpdatesAndReturnsExistingRecipe()
    {
        var recipe = new Recipe { RecipeId = 1, Name = "Old name" };
        var request = new UpdateRecipeRequest("New name", null, null, null, null);
        _recipeRepositoryMock
            .Setup(r => r.GetRecipeByRecipeIdAsync(recipe.RecipeId, UserId))
            .ReturnsAsync(recipe);

        var result = await _recipeService.UpdateRecipeAsync(recipe.RecipeId, request, UserId);

        Assert.That(result, Is.SameAs(recipe));
        Assert.That(recipe.Name, Is.EqualTo(request.Name));
        _recipeRepositoryMock.Verify(r => r.UpdateRecipeAsync(recipe), Times.Once);
    }

    [Test]
    // Checks that deleting an existing recipe removes it and returns true
    public async Task DeleteRecipe_DeletesExistingRecipeAndReturnsTrue()
    {
        var recipe = new Recipe { RecipeId = 1, Name = "Pizza" };
        _recipeRepositoryMock
            .Setup(r => r.GetRecipeByRecipeIdAsync(recipe.RecipeId, UserId))
            .ReturnsAsync(recipe);

        var result = await _recipeService.DeleteRecipeAsync(recipe.RecipeId, UserId);

        Assert.That(result, Is.True);
        _recipeRepositoryMock.Verify(r => r.DeleteRecipeAsync(recipe), Times.Once);
    }
}
