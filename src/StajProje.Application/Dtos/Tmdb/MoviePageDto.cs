using System.Text.Json.Serialization;

namespace StajProje.Application.Dtos.Tmdb;

public class MoviePageDto
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<MovieDto> Results { get; set; } = new();

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

}
