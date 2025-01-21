using TicketEase.Service.TicketPurchase.Entities;

namespace TicketEase.Service.TicketPurchase.Repositories
{
    public interface IFunctionRepository : IRepository<Function>
    {
        Task<IEnumerable<Function>> GetFunctionsByDateAsync(string functionDate);
        Task<IEnumerable<Function>> GetUpcomingFunctionsAsync(DateTime fromDate);

    }
}