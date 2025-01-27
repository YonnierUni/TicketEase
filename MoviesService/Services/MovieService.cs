using AutoMapper;
using MassTransit;
using TicketEase.Service.Movies.Entities;
using TicketEase.Service.Movies.Models;
using TicketEase.Service.Movies.Repositories;
using TicketEase.Service.Movies.Events;

namespace TicketEase.Service.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public MovieService(IMovieRepository movieRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieByIdAsync(Guid movieId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Película con ID {movieId} no encontrada.");
            }

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task AddMovieAsync(MovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await _movieRepository.AddMovieAsync(movie);

            // Publicamos un evento indicando que una película ha sido agregada
            await _publishEndpoint.Publish(new MovieAddedEvent
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                Cast = movie.Cast,
                PosterImage = movie.PosterImage,
                Year = movie.Year
            });
        }

        public async Task UpdateMovieAsync(Guid movieId, MovieDto movieDto)
        {
            if (movieId != movieDto.MovieId)
            {
                throw new ArgumentException("El ID de la película no coincide.");
            }

            var movie = _mapper.Map<Movie>(movieDto);
            await _movieRepository.UpdateMovieAsync(movie);

            // Publicamos un evento indicando que una película ha sido actualizada
            await _publishEndpoint.Publish(new MovieUpdatedEvent
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                Cast = movie.Cast,
                PosterImage = movie.PosterImage,
                Year = movie.Year
            });
        }

        public async Task DeleteMovieAsync(Guid movieId)
        {
            await _movieRepository.DeleteMovieAsync(movieId);

            // Publicamos un evento indicando que una película ha sido eliminada
            await _publishEndpoint.Publish(new MovieDeletedEvent
            {
                MovieId = movieId
            });
        }
    }
}
