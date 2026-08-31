using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cookbook_app.Models;

public class Recipe
{
    
    [Key]
    public string RecipeId { get; set;}
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<string> Ingredients { get; set; }
    
    public List<string> Instructions { get; set; }
    
    
    public float RecipeScore { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }


    public Recipe()
    {
        
    }
    
    
    
}