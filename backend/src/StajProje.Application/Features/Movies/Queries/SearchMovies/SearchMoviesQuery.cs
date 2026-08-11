using MediatR;
using StajProje.Application.Dtos.Tmdb;

namespace StajProje.Application.Features.Movies.Queries.SearchMovies;

public class SearchMoviesQuery : IRequest<MoviePageDto>
{
    public string Query { get; set; } = string.Empty;
}