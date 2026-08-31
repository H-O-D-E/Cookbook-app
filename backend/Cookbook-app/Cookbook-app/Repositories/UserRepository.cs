using Cookbook_app.Data;
using Cookbook_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_app.Repositories;

public class UserRepository : IUserRepository
{

    private readonly CookbookDbContext _context;

    public UserRepository(CookbookDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id
        );
    }

    public Task AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}