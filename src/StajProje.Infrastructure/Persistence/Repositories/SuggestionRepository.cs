using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;
using StajProje.Infrastructure.Persistence;

namespace StajProje.Infrastructure.Persistence.Repositories;

public class SuggestionRepository : ISuggestionRepository
{
    private readonly AppDbContext _context;

    public SuggestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Suggestion suggestion)
    {
        _context.Suggestions.Add(suggestion);
        await _context.SaveChangesAsync();
        return suggestion.Id;
    }
}