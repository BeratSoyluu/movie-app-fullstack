using Microsoft.EntityFrameworkCore;
using StajProje.Application.Interfaces;
using StajProje.Infrastructure.Persistence;
using StajProje.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args); //? Uygulamayı kuran ana yapı

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); //? API dokümantasyonu 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); //? API dokümantasyonu
}

app.UseHttpsRedirection(); //? HTTP'yi HTTPS'e yönlendirir

app.Run(); //? Uygulamayı çalıştırır.