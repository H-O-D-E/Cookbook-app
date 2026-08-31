using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Data;

public class CookbookDbContext : DbContext
{

    public CookbookDbContext(DbContextOptions<CookbookDbContext> options) : base (options)
    {
        
    }
    
    //DBSETS kommer her
    
    
    
}