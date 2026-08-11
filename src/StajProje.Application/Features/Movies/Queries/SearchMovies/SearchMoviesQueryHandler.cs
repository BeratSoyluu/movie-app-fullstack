using MediatR;
using StajProje.Application.Dtos.Tmdb;
using StajProje.Application.Interfaces;

namespace StajProje.Application.Features.Movies.Queries.SearchMovies;

public class SearchMoviesQueryHandler  : IRequestHandler<SearchMoviesQuery, MoviePageDto>
{
    private readonly ITmdbService _tmdbService;

    public SearchMoviesQueryHandler(ITmdbService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    public async Task<MoviePageDto> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _tmdbService.SearchMoviesAsync(request.Query);
    }
}