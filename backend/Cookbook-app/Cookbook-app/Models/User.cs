namespace Cookbook_app.Models;

public class User
{
    
    public int UserId { get; set; }
    
    
    public string Name { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string Email { get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User()
    {
        
    }

   
}