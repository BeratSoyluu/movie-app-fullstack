using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Suggestions.Commands.CreateSuggestion;
using StajProje.Application.Features.Suggestions.Queries.GetMySuggestions;

namespace StajProje.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Movie,Admin")]

public class SuggestionsController : ControllerBase
{
    private readonly ISender _sender;

    public SuggestionsController (ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetMySuggestions()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var query = new GetMySuggestionsQuery { UserId = userId };
        var result = await _sender.Send(query);
        return Ok(result);
    }
}