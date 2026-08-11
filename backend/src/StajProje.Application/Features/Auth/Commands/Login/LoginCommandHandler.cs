using MediatR;
using StajProje.Application.Interfaces;
/*
Handler ne yapacak?
    Gelen kullanıcı adına göre kullanıcıyı bul (UserRepository ile)
    Kullanıcı yoksa VEYA şifre yanlışsa → reddet (hata fırlat)
    Her şey doğruysa → token üret (JwtTokenGenerator ile) ve döndür
*/

namespace StajProje.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.UserName); //! request.UserName (kullanıcının girdiği kullanıcı adı) ile repository'ye "bu kullanıcıyı bul" diyoruz.

        if(user is null || user.Password != request.Password) //! kullanıcı yok VEYA şifre yanlış
        {
            throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı!");
            //! UnauthorizedAccessException --> Yetkisiz erişim hatası
        }

        return _tokenGenerator.GenerateToken(user);
    }
}