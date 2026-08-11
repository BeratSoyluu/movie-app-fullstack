using System.Text.Json.Serialization;

namespace StajProje.Application.Dtos.Tmdb;

public class ActorPageDto
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<ActorDto> Results { get; set; } = new();

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}