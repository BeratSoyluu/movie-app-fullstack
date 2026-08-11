using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Movies.Queries.GetMovieById;
using StajProje.Application.Features.Movies.Queries.GetPopularMovies;
using StajProje.Application.Features.Movies.Queries.SearchMovies;

namespace StajProje.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Movie,Admin")]

public class MoviesController : ControllerBase
{
    private readonly ISender _sender;

    public MoviesController (ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1)
    {
        var query = new GetPopularMoviesQuery { Page = page };
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMovies([FromQuery] string query)
    {
        var q = new SearchMoviesQuery { Query = query };
        var result = await _sender.Send(q);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var query = new GetMovieByIdQuery { MovieId = id, UserId = userId };
        var result = await _sender.Send(query);
        return Ok(result);
    }
}