using System.Runtime.InteropServices.JavaScript;

namespace Cookbook_app.Models;

public class User
{
    
    public int UserId { get; set; }
    
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User()
    {
        
    }

   
}