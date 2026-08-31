using Cookbook_app.Models;

namespace Cookbook_app.Repositories;

public interface IUserRepository
{

    Task<User> GetUserById(int id);
    
    Task AddUser(User user);

    Task DeleteUser(User user);

    Task UpdateUser(User user);


}