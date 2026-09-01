using Cookbook_app.DTOs.Requests;
using Cookbook_app.DTOs.Responses;
using Cookbook_app.Models;
using Cookbook_app.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Cookbook_app.Services;

public class UserService : IUserService
{

    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository repository, IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }


    public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
    {

        var user = new User
        {

            Name = request.Name,
            Email = request.Email,
            CreatedAt = DateTime.Now

        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        await _repository.AddUserAsync(user);

        var dto = new UserDto()
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };

            
        return dto;

        
    }
}