using StajProje.Domain.Entities;

namespace StajProje.Application.Interfaces;
/*
Veritabanı işlemleri asenkron oluyor.
*/
public interface IReviewRepository
{
    Task<int> AddAsync(Review review);
}