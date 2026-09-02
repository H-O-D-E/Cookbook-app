using Cookbook_app.DTOs.Requests;
using Cookbook_app.DTOs.Responses;

namespace Cookbook_app.Services;

public interface IUserService
{

    /// <summary>
    ///Method that takes in a user request 
    /// </summary>
    /// <param name="request"> Parameters necessary to create a user</param>
    /// <returns> Returns a UserDto</returns>
    Task<UserDto> CreateUserAsync(CreateUserRequest request);

}