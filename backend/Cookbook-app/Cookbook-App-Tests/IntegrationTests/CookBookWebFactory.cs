using Cookbook_app.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace CookBook.Tests;

public class CookBookWebFactory : WebApplicationFactory<Program>
{
    public PostgreSqlContainer DatabaseContainer { get; }

    public CookBookWebFactory()
    {
        DatabaseContainer = new PostgreSqlBuilder("postgres:17")
            .WithDatabase("cookbook_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<CookbookDbContext>>();
            services.RemoveAll<CookbookDbContext>();

            services.AddDbContext<CookbookDbContext>(options =>
            {
                options.UseNpgsql(DatabaseContainer.GetConnectionString());
            });
        });
    }
}