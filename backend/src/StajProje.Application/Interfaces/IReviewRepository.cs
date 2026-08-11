using StajProje.Domain.Entities;

namespace StajProje.Application.Interfaces;
/*
Veritabanı işlemleri asenkron oluyor.
*/
public interface IReviewRepository
{
    Task<int> AddAsync(Review review);
    Task<List<Review>> GetByMovieIdAsync(int movieId); // şu filme ait tüm review'leri getir.
    Task<Review?> GetUserReviewAsync(int movieId, int userId); // şu kullanıcının şu filme verdiği review'i getir.
}