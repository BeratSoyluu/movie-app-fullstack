namespace StajProje.Application.Dtos;

public class MovieDetailDto
{
    // TMDB'den gelen film bilgileri
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string? PosterPath { get; set; }
    public double TmdbVoteAverage { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;

    // Veritabanından hesaplanan/gelen bilgiler
    public double AverageScore { get; set; }
    public int? UserScore { get; set; }
    public List<string> Notes { get; set; } = new();
}