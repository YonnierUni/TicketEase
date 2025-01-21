using TicketEase.Service.TicketPurchase.Entities;

namespace TicketEase.Service.TicketPurchase.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTicketsByFunctionIdAsync(Guid functionId);
        Task<IEnumerable<Ticket>> GetTicketsByUserNameAsync(string userName);
    }
}
