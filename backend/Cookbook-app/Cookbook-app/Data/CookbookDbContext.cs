using Cookbook_app.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Data;

public class CookbookDbContext : IdentityDbContext
{

    public CookbookDbContext(DbContextOptions<CookbookDbContext> options) : base (options)
    {
        
    }
    
    //DBSETS kommer her
    
 
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeBook> RecipeBooks { get; set; }
    
    
    
    
}