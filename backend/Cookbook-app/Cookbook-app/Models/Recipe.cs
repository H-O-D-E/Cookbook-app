using System.ComponentModel.DataAnnotations;

namespace Cookbook_app.Models;

public class Recipe
{
    public int RecipeId { get; set;}
    
    public string Name { get; set; }

    public string Description { get; set; }

    [Url]
    [Display(Name = "Recipe Image URL")]
    public string ImageUrl { get; set; }

    public string Ingredients { get; set; }

    public string Instructions { get; set; }

    public float RecipeScore { get; set; } = 0;

    public int RecipeBookId { get; set; }
    
    public RecipeBook RecipeBook { get; set; }
}