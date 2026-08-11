using StajProje.Domain.Entities;

namespace StajProje.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}