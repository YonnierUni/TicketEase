using TicketEase.Service.TicketPurchase.Entities;

namespace TicketEase.Service.TicketPurchase.Repositories
{
    public interface IFunctionRepository
    {
        Task<IEnumerable<Function>> GetAllAsync();
        Task<Function> GetByIdAsync(Guid functionId);
        Task<Function> AddAsync(Function function);
        Task<Function> UpdateAsync(Function function);
        Task DeleteAsync(Guid functionId);
        Task<IEnumerable<Function>> GetFunctionsByDateAsync(string functionDate);
        Task<IEnumerable<Function>> GetUpcomingFunctionsAsync(DateTime fromDate);
    }
}