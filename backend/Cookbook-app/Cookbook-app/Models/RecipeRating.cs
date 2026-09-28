using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Cookbook_app.Models;

public class RecipeRating
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public IdentityUser User { get; set; } = null!;

    [Range(1, 5)]
    public int Score { get; set; }
}