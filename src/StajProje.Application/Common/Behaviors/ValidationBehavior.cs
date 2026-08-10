using System.Runtime.CompilerServices;
using FluentValidation;
using MediatR;

namespace StajProje.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators= validators;
    }

    public async Task<TResponse> Handle( // Handle metodu — asıl iş: MediatR command'i handler'a göndermeden önce bunu çağırır.
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any()) // "bu command'in validator'ı var mı?" Varsa kontrol et, yoksa geç.
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll( // tüm validator'ları çalıştır (birden fazla varsa hepsini).
                            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));        
            
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if(failures.Count != 0) // ihlal varsa hata fırlat.
            {
                throw new ValidationException(failures);
            }
        }
        return await next(); // ihlal yoksa devam et. next() = "sıradaki adıma geç"
    }
}