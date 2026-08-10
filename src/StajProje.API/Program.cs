using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StajProje.API.Middleware;
using StajProje.Application.Interfaces;
using StajProje.Infrastructure.Authentication;
using StajProje.Infrastructure.Persistence;
using StajProje.Infrastructure.Persistence.Repositories;
using FluentValidation;

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

builder.Services.AddValidatorsFromAssembly(
    typeof(StajProje.Application.Interfaces.IUserRepository).Assembly);

builder.Services.AddTransient(
    typeof(MediatR.IPipelineBehavior<,>),
    typeof(StajProje.Application.Common.Behaviors.ValidationBehavior<,>));

builder.Services.AddControllers(); //? Controller desteği

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT token'ınızı girin (Bearer olmadan sadece token)"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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