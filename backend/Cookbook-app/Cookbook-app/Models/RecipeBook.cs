namespace Cookbook_app.Models;

public class RecipeBook
{
    
    public int RecipeBookId { get; set; }
    
    public List<Recipe> ListOfRecipes { get; set; } = new();
    
    public string Name { get; set; }
    
    public float RecipeBookScore { get; set; }

    public RecipeBook()
    {
    }
}