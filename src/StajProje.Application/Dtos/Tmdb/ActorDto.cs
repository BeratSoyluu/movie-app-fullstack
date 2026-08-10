using System.Text.Json.Serialization;

namespace StajProje.Application.Dtos.Tmdb;

public class ActorDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
}