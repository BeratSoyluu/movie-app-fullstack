using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Suggestions.Commands.CreateSuggestion;

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

    [HttpPost]
    public async Task<IActionResult> CreateSuggestion(CreateSuggestionCommand command)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        command.SuggestedByUserId = userId;

        var suggestionId = await _sender.Send(command);
        return Ok(suggestionId);
    }
}