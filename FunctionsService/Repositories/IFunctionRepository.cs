using TicketEase.Service.Functions.Entities;

namespace TicketEase.Service.Functions.Repositories
{
    public interface IFunctionRepository
    {
        Task<IEnumerable<MovieFunction>> GetAllFunctionsAsync();
        Task<MovieFunction> GetFunctionByIdAsync(Guid id);
        Task AddFunctionAsync(MovieFunction function);
        Task UpdateFunctionAsync(MovieFunction function);
        Task DeleteFunctionAsync(Guid id);
    }
}
