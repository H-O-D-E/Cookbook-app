using Microsoft.AspNetCore.Identity;

namespace Cookbook_app.Models;

public class RecipeBook
{
    public int RecipeBookId { get; set; }

    public string Name { get; set; }

    public float RecipeBookScore { get; set; } = 0;

    public string UserId { get; set; }
    
    public IdentityUser User { get; set; }

    public List<Recipe> Recipes { get; set; } = new();
}