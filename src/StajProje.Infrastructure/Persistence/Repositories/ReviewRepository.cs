using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;
using StajProje.Infrastructure.Persistence;

namespace StajProje.Infrastructure.Persistence.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<int> AddAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review.Id;
    }
}