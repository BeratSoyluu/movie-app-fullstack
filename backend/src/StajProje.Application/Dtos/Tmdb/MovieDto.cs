using System.Text.Json.Serialization;

namespace StajProje.Application.Dtos.Tmdb;

public class MovieDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;

}
