/*
Handler'ın görevi nedir?
    Handler, Command'i alıp asıl işi yapan sınıf. Bizim Handler'ımız şunları yapacak:
    - Gelen CreateSuggestionCommand'i al (içinde film adı + kullanıcı id'si var)
    - Bir Suggestion entity'si oluştur (Command'deki bilgiyi entity'ye aktar + CreatedAt'i "şu an" yap)
    - Repository'ye "bunu kaydet" de
    - Dönen id'yi geri ver


*/

using MediatR;
using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;

namespace StajProje.Application.Features.Suggestions.Commands.CreateSuggestion;

public class CreateSuggestionCommandHandler : IRequestHandler<CreateSuggestionCommand, int>
{
    private readonly ISuggestionRepository _repository;

    public CreateSuggestionCommandHandler(ISuggestionRepository repository) // constructor
    { // Burada diyoruz ki: "Beni oluştururken bana bir ISuggestionRepository verin, adına repository diyeceğim."
        _repository = repository;
    }


    public async Task<int> Handle(CreateSuggestionCommand request, CancellationToken cancellationToken)
    {
        var suggestion = new Suggestion
        {
            MovieName = request.MovieName,
            SuggestedByUserId = request.SuggestedByUserId,
            CreatedAt = DateTime.UtcNow
        };
        return await _repository.AddAsync(suggestion);
    }
}