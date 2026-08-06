using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;

namespace StajProje.Infrastructure.Authentication;
/*
Çünkü token üretirken appsettings'teki Jwt:Key, Jwt:Issuer gibi ayarları okuyacağız. IConfiguration o okuma kapımız.
*/
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var claims = new[] //! Açıklayayım — bu, JWT'nin payload kısmını (hatırla, token'ın "bilgi taşıyan" bölümü) hazırlıyor
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)); //1
        //! İki satır var, ikisini de açıklayayım — bu, JWT'nin signature (imza) kısmını hazırlıyor. Hatırla, imza gizli anahtarla yapılıyordu ve token'ın sahteliğini engelliyordu. İşte o mekanizmayı burada kuruyoruz.
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //2

        var token = new JwtSecurityToken( //! JWT'nin tüm parçalarını birleştiren yer
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
