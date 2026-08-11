using MediatR;

namespace StajProje.Application.Features.Suggestions.Commands.CreateSuggestion;

/*
IRequest --> Bir sınıfa bu etiketi yapıştırınca, MediatR ona bakıp "aha, bu bir istek (request), ben bunu işleyebilirim" diyor. Yani bu etiket, sınıfı MediatR'ın tanıyabileceği bir şeye dönüştürüyor.
<int> --> "bu isteği işle ve bana bir int (id) döndür" diyorsun.

"Ben bir öneri isteğiyim (IRequest etiketim var, MediatR beni tanır). İçimde iki bilgi taşıyorum: hangi film önerildi (MovieName) ve kim önerdi (SuggestedByUserId). İşim bitince geriye bir id (int) döneceğim."
*/

public class CreateSuggestionCommand : IRequest<int>
{
    public string MovieName { get; set; } = string.Empty; // Hangi film önerildi
    public int SuggestedByUserId { get; set; } // kim önerdi
}