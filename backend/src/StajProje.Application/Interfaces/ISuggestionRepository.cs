using StajProje.Domain.Entities;

namespace StajProje.Application.Interfaces;
/*
Veritabanı işlemleri asenkron oluyor.


*/
public interface ISuggestionRepository
{
    Task<int> AddAsync(Suggestion suggestion);
    Task<List<Suggestion>> GetByUserIdAsync(int userId);
}