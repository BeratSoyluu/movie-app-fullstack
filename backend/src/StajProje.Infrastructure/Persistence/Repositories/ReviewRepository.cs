using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Review>> GetByMovieIdAsync(int movieId) // bir filme ait tüm review'ler
    {
        return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .ToListAsync();
    }

    public async Task<Review?> GetUserReviewAsync(int movieId, int userId) // kullanıcının o filme verdiği review
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);
    }
}