using Cookbook_app.DTOs.RequestDTO;
using Cookbook_app.Models;
using Cookbook_app.Repositories;
using Cookbook_app.Services;
using Moq;
using NUnit.Framework;

namespace RecipeBookService_Tests;

public class RecipeBookServiceTests
{
    private const string UserId = "user 1";
    private Mock<IRecipeBookRepository> _recipeBookRepositoryMock = null!;
    private RecipeBookService _recipeBookService = null!;

    [SetUp]
    public void Setup()
    {
        _recipeBookRepositoryMock = new Mock<IRecipeBookRepository>();
        _recipeBookService = new RecipeBookService(_recipeBookRepositoryMock.Object);
    }

    [Test]
    // Checks that a user can get a recipe book that belongs to them
    public async Task GetRecipeBook_ReturnsBookOwnedByUser()
    {
        var book = new RecipeBook { RecipeBookId = 1, Name = "chicken nuggets ", UserId = UserId };
        _recipeBookRepositoryMock.Setup(r => r.GetRecipeBookByIdAsync(book.RecipeBookId, UserId)).ReturnsAsync(book);

        var result = await _recipeBookService.GetRecipeBookAsync(book.RecipeBookId, UserId);

        Assert.That(result, Is.SameAs(book));
    }

    [Test]
    // Checks that when  creating a recipe book returns it and saves it for the user
    public async Task CreateRecipeBook_ReturnsAndSavesNewBook()
    {
        var request = new CreateRecipeBookRequest("Dinner");
        _recipeBookRepositoryMock.Setup(r => r.GetRecipeBookByNameAsync(request.RecipeBookName, UserId)).ReturnsAsync((RecipeBook?)null);

        var result = await _recipeBookService.CreateRecipeBookAsync(request, UserId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(request.RecipeBookName));
        _recipeBookRepositoryMock.Verify(r => r.AddRecipeBookAsync(It.Is<RecipeBook>(book =>
            book.Name == request.RecipeBookName && book.UserId == UserId)), Times.Once);
    }

    [Test]
    // Checking  that it  can rename their recipe book and save the new name
    public async Task UpdateRecipeBook_UpdatesAndReturnsBookOwnedByUser()
    {
        var book = new RecipeBook { RecipeBookId = 1, Name = "Old name", UserId = UserId };
        var request = new UpdateRecipeBookRequest("New name");
        _recipeBookRepositoryMock.Setup(r => r.GetRecipeBookByIdAsync(book.RecipeBookId, UserId)).ReturnsAsync(book);

        var result = await _recipeBookService.UpdateRecipeBookAsync(book.RecipeBookId, request, UserId);

        Assert.That(result, Is.SameAs(book));
        Assert.That(book.Name, Is.EqualTo(request.Name));
        _recipeBookRepositoryMock.Verify(r => r.UpdateRecipeBookAsync(book), Times.Once);
    }

    [Test]
    // Checks that  it delete their recipe book and gets a successful result
    public async Task DeleteRecipeBook_DeletesBookOwnedByUserAndReturnsTrue()
    {
        var book = new RecipeBook { RecipeBookId = 1, Name = "Dinner", UserId = UserId };
        _recipeBookRepositoryMock.Setup(r => r.GetRecipeBookByIdAsync(book.RecipeBookId, UserId)).ReturnsAsync(book);

        var result = await _recipeBookService.DeleteRecipeBookAsync(book.RecipeBookId, UserId);

        Assert.That(result, Is.True);
        _recipeBookRepositoryMock.Verify(r => r.DeleteRecipeBookAsync(book), Times.Once);
    }
}
