using TicketEase.Service.Functions.Models;

namespace TicketEase.Service.Functions.Services
{
    public interface IFunctionsService
    {
        Task<IEnumerable<MovieFunctionDto>> GetAllFunctionsAsync();
        Task<MovieFunctionDto> GetFunctionByIdAsync(Guid id);
        Task AddFunctionAsync(MovieFunctionDto functionDto);
        Task<IEnumerable<MovieFunctionDto>> GetFunctionsByMovieIdAsync(Guid movieId);
        Task<IEnumerable<MovieFunctionDto>> GetFunctionsByIdsAsync(IEnumerable<Guid> ids);

        Task UpdateFunctionAsync(Guid id, MovieFunctionDto functionDto);
        Task DeleteFunctionAsync(Guid id);

    }
}
