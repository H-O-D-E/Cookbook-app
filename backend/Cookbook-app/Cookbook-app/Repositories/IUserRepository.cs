using Cookbook_app.Models;

namespace Cookbook_app.Repositories;

public interface IUserRepository
{

   
    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByEmailAsync(string email);
    
    Task AddUserAsync(User user);

    Task DeleteUserAsync(User user);

    Task UpdateUserAsync(User user);


}