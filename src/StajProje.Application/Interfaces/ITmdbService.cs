using StajProje.Application.Dtos.Tmdb;

namespace StajProje.Application.Interfaces;

/*
page parametresi (kaçıncı sayfa — sayfalama için), Task<MoviePageDto> döndürür
*/
public interface ITmdbService
{
    Task<MoviePageDto> GetPopularMoviesAsync(int page);
    Task<ActorPageDto> GetPopularActorsAsync(int page);
}