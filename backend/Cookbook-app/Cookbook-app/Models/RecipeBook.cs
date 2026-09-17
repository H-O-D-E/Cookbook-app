using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cookbook_app.Models;

public class RecipeBook
{
    public int RecipeBookId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [Url]
    [Display(Name = "RecipeBook Image URL")]
    public string ImageUrl { get; set; }

    public float RecipeBookScore { get; set; } = 0;

    public string UserId { get; set; }
    
    public IdentityUser User { get; set; }

    public List<Recipe> Recipes { get; set; } = new();
}