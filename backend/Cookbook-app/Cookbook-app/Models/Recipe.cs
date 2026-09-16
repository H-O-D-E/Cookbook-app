// REVIEW(noob): a stray 'using System.Threading.RateLimiting;' that nothing needs, and this class sits in the global namespace while every other model is in Cookbook_app.Models. Put it in the same namespace as RecipeBook.
// REVIEW(noob): the string properties are non-nullable with no initialiser and no [Required], so with Nullable enabled every one of them is a compiler warning, and EF infers the column nullability from them. Add 'required' or '= string.Empty;' and be explicit about the constraints.
using System.Threading.RateLimiting;using Cookbook_app.Models;

public class Recipe
{
    public int RecipeId { get; set;}
    
    public string Name { get; set; }

    public string Description { get; set; }

    public string Ingredients { get; set; }

    public string Instructions { get; set; }

    public float RecipeScore { get; set; } = 0;

    public int RecipeBookId { get; set; }
    
    public RecipeBook RecipeBook { get; set; }
}