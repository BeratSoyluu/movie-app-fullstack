using MediatR;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Auth.Commands.Login;

/*
ISender nedir? MediatR'ın "command/query gönderme" arayüzü. _sender.Send(command) dediğinde, MediatR o command'i alıp doğru handler'a götürür (postane, hatırla). Yani controller'ın MediatR'a erişim kapısı.
*/

namespace StajProje.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender; // MediatR'ı tutacağımız alan

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")] // Neden POST, GET değil? Çünkü login'de kullanıcı adı/şifre gönderiyoruz (veri yolluyoruz). GET veri almak için, POST veri göndermek için. Şifre gibi hassas veri URL'de görünmesin diye de POST.
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var token = await _sender.Send(command);
        return Ok(token);
    }
}