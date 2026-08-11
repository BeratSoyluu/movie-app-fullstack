using StajProje.Domain.Entities;

namespace StajProje.Application.Interfaces;
/*
User? --> ya bir User döner, ya da (bulunamazsa) null.
*/
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}