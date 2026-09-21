using System.ComponentModel.DataAnnotations;

namespace Cookbook_app.Models.Auth;

public record LoginRequest(
    [Required(ErrorMessage = "Username is required.")]
    string Username, 
    
    [Required(ErrorMessage = "Username is required.")]
    string Password
);