using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StajProje.API.Middleware;
using StajProje.Application.Interfaces;
using StajProje.Infrastructure.Authentication;
using StajProje.Infrastructure.Persistence;
using StajProje.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args); //? Uygulamayı kuran ana yapı

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddOpenApi(); //? API dokümantasyonu

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(StajProje.Application.Interfaces.IUserRepository).Assembly));

builder.Services.AddControllers(); //? Controller desteği

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //? Swagger UI

//? JWT ile kimlik doğrulama ayarı
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); //? API dokümantasyonu
    app.UseSwagger();       //? Swagger JSON
    app.UseSwaggerUI();     //? Swagger görsel arayüz
}

app.UseHttpsRedirection(); //? HTTP'yi HTTPS'e yönlendirir

app.UseAuthentication(); //? Token'ı kontrol eder (kim bu kullanıcı?)
app.UseAuthorization();  //? Yetki kontrolü (bu kullanıcı bunu yapabilir mi?)

app.MapControllers(); //? Controller'ları endpoint olarak bağlar

app.Run(); //? Uygulamayı çalıştırır.