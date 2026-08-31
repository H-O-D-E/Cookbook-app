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
    
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users
            .Remove(user);

        await _context.SaveChangesAsync();

    }

    public async Task UpdateUserAsync(User user)
    {

        _context.Users
            .Update(user);

        await _context.SaveChangesAsync();

    }
}