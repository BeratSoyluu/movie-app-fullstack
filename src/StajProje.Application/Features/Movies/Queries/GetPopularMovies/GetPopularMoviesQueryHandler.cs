using MediatR;
using StajProje.Application.Dtos.Tmdb;
using StajProje.Application.Interfaces;

namespace StajProje.Application.Features.Movies.Queries.GetPopularMovies;

public class GetPopularMoviesQueryHandler : IRequestHandler<GetPopularMoviesQuery, MoviePageDto>
{
    private readonly ITmdbService _tmdbService;

    public GetPopularMoviesQueryHandler(ITmdbService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    public async Task<MoviePageDto> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _tmdbService.GetPopularMoviesAsync(request.Page);
    }
}