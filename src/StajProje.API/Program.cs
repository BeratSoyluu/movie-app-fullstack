var builder = WebApplication.CreateBuilder(args); //? Uygulamayı kuran ana yapı

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); //? API dokümantasyonu 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); //? API dokümantasyonu
}

app.UseHttpsRedirection(); //? HTTP'yi HTTPS'e yönlendirir

app.Run(); //? Uygulamayı çalıştırır.