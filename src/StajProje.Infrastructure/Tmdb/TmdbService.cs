using System.Net.Http.Json; // HttpClient'ın JSON'u otomatik DTO'ya çevirme özelliği için
using Microsoft.Extensions.Configuration; // IConfiguration (appsettings'ten TMDB key/URL okumak için)
using StajProje.Application.Dtos.Tmdb; // MoviePageDto için
using StajProje.Application.Interfaces; // ITmdbService için

namespace StajProje.Infrastructure.Tmdb;

public class TmdbService : ITmdbService // : ITmdbService → interface'i uyguluyoruz. Yani GetPopularMoviesAsync'i yazmak zorundayız.
{
    private readonly HttpClient _httpClient; // TMDB'ye istek atacağımız araç. Dışarıdan enjekte ediliyor
    private readonly IConfiguration _configuration; // appsettings'teki TMDB ayarlarını (ApiKey, BaseUrl) okumak için

    public TmdbService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<MoviePageDto> GetPopularMoviesAsync(int page)
    {
        var apiKey = _configuration["Tmdb:ApiKey"]; // Tmdb bölümündeki ApiKey'i getir
        var url = $"movie/popular?api_key={apiKey}&page={page}&language=tr-TR"; // TMDB'ye atacağımız isteğin adresi

        var result = await _httpClient.GetFromJsonAsync<MoviePageDto>(url); // şu URL'e GET isteği at, dönen JSON'u otomatik olarak MoviePageDto'ya çevir.

        return result ?? new MoviePageDto();
    }
}