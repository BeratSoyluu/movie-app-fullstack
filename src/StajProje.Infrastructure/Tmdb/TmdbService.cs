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

    private const int MaxItems = 100;

    public async Task<MoviePageDto> GetPopularMoviesAsync(int page)
    {
        var apiKey = _configuration["Tmdb:ApiKey"];
        var allMovies = new List<MovieDto>();
        int currentPage = 1;

        while (allMovies.Count < MaxItems)
        {
            var url = $"movie/popular?api_key={apiKey}&page={currentPage}&language=tr-TR";
            var pageResult = await _httpClient.GetFromJsonAsync<MoviePageDto>(url);

            if (pageResult is null || pageResult.Results.Count == 0)
                break;

            allMovies.AddRange(pageResult.Results);
            currentPage++;

            if (currentPage > pageResult.TotalPages)
                break;
        }

        return new MoviePageDto
        {
            Page = 1,
            Results = allMovies.Take(MaxItems).ToList(),
            TotalPages = 1
        };
    }

    public async Task<ActorPageDto> GetPopularActorsAsync(int page)
    {
        var apiKey = _configuration["Tmdb:ApiKey"];
        var allActors = new List<ActorDto>();
        int currentPage = 1;

        while (allActors.Count < MaxItems)
        {
            var url = $"person/popular?api_key={apiKey}&page={currentPage}&language=tr-TR";
            var pageResult = await _httpClient.GetFromJsonAsync<ActorPageDto>(url);

            if (pageResult is null || pageResult.Results.Count == 0)
                break;

            allActors.AddRange(pageResult.Results);
            currentPage++;

            if (currentPage > pageResult.TotalPages)
                break;
        }

        return new ActorPageDto
        {
            Page = 1,
            Results = allActors.Take(MaxItems).ToList(),
            TotalPages = 1
        };
    }

    public async Task<MovieDto> GetMovieByIdAsync(int movieId)
    {
        var apiKey = _configuration["Tmdb:ApiKey"];
        var url = $"movie/{movieId}?api_key={apiKey}&language=tr-TR";

        var result = await _httpClient.GetFromJsonAsync<MovieDto>(url);

        return result ?? new MovieDto();
    }

    public async Task<MoviePageDto> SearchMoviesAsync(string query)
    {
        var apiKey = _configuration["Tmdb:ApiKey"];
        var url = $"search/movie?api_key={apiKey}&query={query}&language=tr-TR";
        var result = await _httpClient.GetFromJsonAsync<MoviePageDto>(url);
        
        return result ?? new MoviePageDto();
    }

    public async Task<ActorPageDto> SearchActorsAsync(string query)
    {
        var apiKey = _configuration["Tmdb:ApiKey"];
        var url = $"search/person?api_key={apiKey}&query={query}&language=tr-TR";
        var result = await _httpClient.GetFromJsonAsync<ActorPageDto>(url);
        
        return result ?? new ActorPageDto();
    }
}