using System.ComponentModel.DataAnnotations;

namespace Cookbook_app.Models.Auth;

public record RegisterUserRequest(
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage="Username must be between 3 and 20 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Only letters, numbers, and underscores are allowed.")]
    string Username, 

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    string Email, 

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [DataType(DataType.Password)]
    string Password
);