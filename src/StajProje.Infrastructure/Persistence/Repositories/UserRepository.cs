using Microsoft.EntityFrameworkCore;
using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;
using StajProje.Infrastructure.Persistence;

namespace StajProje.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}