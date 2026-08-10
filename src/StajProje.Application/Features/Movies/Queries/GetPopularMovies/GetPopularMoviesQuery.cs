using MediatR;
using StajProje.Application.Dtos.Tmdb;

namespace StajProje.Application.Features.Movies.Queries.GetPopularMovies;

public class GetPopularMoviesQuery : IRequest<MoviePageDto>
{
    public int Page { get; set; }
}