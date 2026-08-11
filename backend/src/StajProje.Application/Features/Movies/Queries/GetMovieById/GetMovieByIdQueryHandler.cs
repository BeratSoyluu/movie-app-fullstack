using System.Xml.Schema;
using MediatR;
using StajProje.Application.Dtos;
using StajProje.Application.Interfaces;

namespace StajProje.Application.Features.Movies.Queries.GetMovieById;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDetailDto>
{
    private readonly ITmdbService _tmdbService; // TMDB'den film çekmek için
    private readonly IReviewRepository _reviewRepository; // veritabanından review'ler için

    public GetMovieByIdQueryHandler(ITmdbService tmdbService, IReviewRepository reviewRepository)
    {
        _tmdbService = tmdbService;
        _reviewRepository = reviewRepository;
    }

    public async Task<MovieDetailDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _tmdbService.GetMovieByIdAsync(request.MovieId); // Query'den gelen MovieId ile TMDB'ye gidip filmi çekiyoruz.
        var reviews = await _reviewRepository.GetByMovieIdAsync(request.MovieId); // Aynı MovieId ile veritabanından o filme ait tüm review'leri çekiyoruz.

        double averageScore = reviews.Any() // Ortalama puanı hesapla
            ? reviews.Average(r => r.Score)
            : 0;

        var userReview = await _reviewRepository.GetUserReviewAsync(request.MovieId,request.UserId); // Bu kullanıcının (UserId) o filme (MovieId) verdiği kendi review'ini çekiyoruz.

        var notes = reviews
            .Where(r => !string.IsNullOrWhiteSpace(r.Note)) // boş olmayan notları süz.
            .Select(r => r.Note)
            .ToList();

        return new MovieDetailDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Overview = movie.Overview,
            PosterPath = movie.PosterPath,
            TmdbVoteAverage = movie.VoteAverage,
            ReleaseDate = movie.ReleaseDate,
            AverageScore = averageScore,
            UserScore = userReview?.Score,
            Notes = notes
        };
    }
}