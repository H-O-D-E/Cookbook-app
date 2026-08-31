using Cookbook_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Data;

public class CookbookDbContext : DbContext
{

    public CookbookDbContext(DbContextOptions<CookbookDbContext> options) : base (options)
    {
        
    }
    
    //DBSETS kommer her
    
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeBook> RecipeBooks { get; set; }
    
    
    
    
}