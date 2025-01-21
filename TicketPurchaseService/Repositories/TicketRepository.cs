using Microsoft.EntityFrameworkCore;
using TicketEase.Service.TicketPurchase.DbContexts;
using TicketEase.Service.TicketPurchase.Entities;

namespace TicketEase.Service.TicketPurchase.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketPurchaseDbContext _ticketPurchaseDbContext;

        public TicketRepository(TicketPurchaseDbContext ticketDbContext)
        {
            _ticketPurchaseDbContext = ticketDbContext;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _ticketPurchaseDbContext.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(Guid ticketId)
        {
            return await _ticketPurchaseDbContext.Tickets.FindAsync(ticketId);
        }

        public async Task AddAsync(Ticket ticket)
        {
            _ticketPurchaseDbContext.Tickets.Add(ticket);
            await _ticketPurchaseDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _ticketPurchaseDbContext.Tickets.Update(ticket);
            await _ticketPurchaseDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid ticketId)
        {
            var ticket = await _ticketPurchaseDbContext.Tickets.FindAsync(ticketId);
            if (ticket != null)
            {
                _ticketPurchaseDbContext.Tickets.Remove(ticket);
                await _ticketPurchaseDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByFunctionIdAsync(Guid functionId)
        {
            return await _ticketPurchaseDbContext.Tickets
                .Where(t => t.FunctionId == functionId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserNameAsync(string userName)
        {
            return await _ticketPurchaseDbContext.Tickets
                .Where(t => t.UserName == userName)
                .ToListAsync();
        }
    }
}
