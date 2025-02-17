﻿using TicketEase.Service.Movies.Models;

namespace TicketEase.Service.Movies.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto> GetMovieByIdAsync(Guid movieId);
        Task<IEnumerable<MovieDto>> GetMoviesByIdsAsync(List<Guid> movieIds);  // Nuevo método

        Task AddMovieAsync(MovieDto movieDto);
        Task UpdateMovieAsync(Guid movieId, MovieDto movieDto);
        Task DeleteMovieAsync(Guid movieId);
    }
}
