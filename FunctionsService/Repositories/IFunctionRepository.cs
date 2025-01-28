using TicketEase.Service.Functions.Entities;

namespace TicketEase.Service.Functions.Repositories
{
    public interface IFunctionRepository
    {
        Task<IEnumerable<MovieFunction>> GetAllFunctionsAsync();
        Task<MovieFunction> GetFunctionByIdAsync(Guid id);
        Task AddFunctionAsync(MovieFunction function);
        Task<IEnumerable<MovieFunction>> GetFunctionsByMovieIdAsync(Guid function);
        Task<IEnumerable<MovieFunction>> GetFunctionsByIdsAsync(IEnumerable<Guid> ids);

        Task UpdateFunctionAsync(MovieFunction function);
        Task DeleteFunctionAsync(Guid id);
    }
}
