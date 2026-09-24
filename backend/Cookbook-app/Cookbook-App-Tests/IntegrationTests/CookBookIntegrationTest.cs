
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Cookbook_app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CookBook.Tests;

[TestFixture]
public class CookBookIntegrationTests
{
    private CookBookWebFactory _factory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public async Task SetUp()
    {
        Environment.SetEnvironmentVariable(
            "Jwt__IssuerSigningKey",
            "2RlIxYQXpPLa3FM+4sy8yKA6jnkfXchCHPCXWdya2EA");

        Environment.SetEnvironmentVariable(
            "Jwt__ValidIssuer",
            "CookbookIntegrationTests");

        Environment.SetEnvironmentVariable(
            "Jwt__ValidAudience",
            "CookbookIntegrationTests");

        _factory = new CookBookWebFactory();

        
        await _factory.DatabaseContainer.StartAsync();

      
        _client = _factory.CreateClient();

       
        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<CookbookDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        _client?.Dispose();

        if (_factory != null)
        {
            await _factory.DatabaseContainer.DisposeAsync();
            _factory.Dispose();
        }

        Environment.SetEnvironmentVariable("Jwt__IssuerSigningKey", null);
        Environment.SetEnvironmentVariable("Jwt__ValidIssuer", null);
        Environment.SetEnvironmentVariable("Jwt__ValidAudience", null);
    }

    [Test]
    public async Task Register_With_ValidDetails_creatUser()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            username = "david",
            email = "david@goat.com",
            password = "David123!"
        });

        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task logIn_After_Registeration_RETURNSJWT()
    {
        await _client.PostAsJsonAsync("/api/auth/register", new
        {
            username = "david",
            email = "david@goat.com",
            password = "David123!"
        });

        var response = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "david",
            password = "David123!"
        });

        var body =
            await response.Content.ReadFromJsonAsync<JsonElement>();

        Assert.Multiple(() =>
        {
            Assert.That(
                response.StatusCode,
                Is.EqualTo(HttpStatusCode.OK));

            Assert.That(
                body.GetProperty("token").GetString(),
                Is.Not.Empty);
        });
    }

    [Test]


    public async Task create_recipe_return_sucess()
    {
         var register = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            username = "david",
            email = "david@goat.com",
            password = "David123!"
        });

        var login = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "david",
            password = "David123!"
        });

        var token = (await login.Content.ReadFromJsonAsync<JsonElement>())
            .GetProperty("token").GetString();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        
        var recipeBookResponse = await _client.PostAsJsonAsync(
            "/api/recipebooks",
            new
            {
                recipeBookName = "meatbalss",
                description = "Italian recipes",
                imageUrl = "hhh"
            });
        
        Assert.That(
            recipeBookResponse.StatusCode,
            Is.EqualTo(HttpStatusCode.Created));
        var recipeBookBody =
            await recipeBookResponse.Content.ReadFromJsonAsync<JsonElement>();

        var recipeBookId = recipeBookBody
            .GetProperty("recipeBookId")
            .GetInt32();
        var recipeResponse = await _client.PostAsJsonAsync(
            $"/api/recipebooks/{recipeBookId}/recipes",
            new
            {
                recipeName = "Pizza",
                description = " pizza",
                imageUrl = "httpsexmaple.com",
                ingredients = "balls",
                instructions = "pray to god"
            });
        
        Assert.That(recipeResponse.StatusCode,Is.EqualTo(HttpStatusCode.Created));
    }
}
