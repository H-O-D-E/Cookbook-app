using System.Threading.RateLimiting;using Cookbook_app.Models;

public class Recipe
{
    public int RecipeId { get; set;}
    
    public string Name { get; set; }

    public string Description { get; set; }

    public string Ingredients { get; set; }

    public string Instructions { get; set; }

    public float RecipeScore { get; set; }

    public int RecipeBookId { get; set; }
    
    public RecipeBook RecipeBook { get; set; }
}