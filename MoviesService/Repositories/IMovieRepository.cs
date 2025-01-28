using TicketEase.Service.Movies.Entities;

namespace TicketEase.Service.Movies.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(Guid movieId);
        Task<IEnumerable<Movie>> GetMoviesByIdsAsync(List<Guid> movieIds);

        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(Guid movieId);
    }
}
