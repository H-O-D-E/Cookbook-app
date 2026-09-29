using Cookbook_app.Models.Auth;
using Cookbook_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Cookbook_app.Models.Auth.LoginRequest;

namespace Cookbook_app.Controllers;


[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;

    public AuthController(UserManager<IdentityUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest registerRequest)
    {
        var usernameExists = await _userManager.FindByNameAsync(registerRequest.Username);
        var emailExists = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (usernameExists is not null || emailExists is not null) {
            return Conflict(new ProblemDetails
            {
                Title = "Username or Email already exists",
                Detail = $"Username or Email already exists",
                Status = StatusCodes.Status409Conflict
            });
        }

        var newUser = new IdentityUser
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email
        };

        var result = await _userManager.CreateAsync(
            newUser, registerRequest.Password
        );

        if (!result.Succeeded)
        {
            return Conflict(new ProblemDetails
            {
                Title = "User could not be registered",
                Detail = $"Password must be longer than 8 characters, require at least one digit, lower- and upper-case letters, and one special character",
                Status = StatusCodes.Status409Conflict
            });
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return Unauthorized();
        }

        var validPassword =
            await _userManager.CheckPasswordAsync(user, request.Password);

        if (!validPassword)
        {
            return Unauthorized();
        }

        var token = _jwtService.CreateToken(user);

        return Ok(new
        {
            token
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        await _userManager.UpdateSecurityStampAsync(user);

        return NoContent();
    }

}