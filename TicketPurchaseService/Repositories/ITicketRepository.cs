using TicketEase.Service.TicketPurchase.Entities;

namespace TicketEase.Service.TicketPurchase.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(Guid ticketId);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Guid ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByFunctionIdAsync(Guid functionId);
        Task<IEnumerable<Ticket>> GetTicketsByUserNameAsync(string userName);
    }
}
