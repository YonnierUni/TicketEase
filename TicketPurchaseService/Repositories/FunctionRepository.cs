using Microsoft.EntityFrameworkCore;
using TicketEase.Service.TicketPurchase.DbContexts;
using TicketEase.Service.TicketPurchase.Entities;


namespace TicketEase.Service.TicketPurchase.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly TicketPurchaseDbContext _ticketPurchaseDbContext;

        public FunctionRepository(TicketPurchaseDbContext context)
        {
            _ticketPurchaseDbContext = context;
        }

        public async Task<IEnumerable<Function>> GetAllAsync()
        {
            return await _ticketPurchaseDbContext.Functions.ToListAsync();
        }

        public async Task<Function> GetByIdAsync(Guid functionId)
        {
            return await _ticketPurchaseDbContext.Functions.FindAsync(functionId);
        }

        public async Task AddAsync(Function function)
        {
            _ticketPurchaseDbContext.Functions.Add(function);
            await _ticketPurchaseDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Function function)
        {
            _ticketPurchaseDbContext.Functions.Update(function);
            await _ticketPurchaseDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid functionId)
        {
            var function = await _ticketPurchaseDbContext.Functions.FindAsync(functionId);
            if (function != null)
            {
                _ticketPurchaseDbContext.Functions.Remove(function);
                await _ticketPurchaseDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Function>> GetFunctionsByDateAsync(string functionDate)
        {
            return await _ticketPurchaseDbContext.Functions
                .Where(f => f.FunctionDate.ToString("yyyy-MM-dd") == functionDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Function>> GetUpcomingFunctionsAsync(DateTime fromDate)
        {
            return await _ticketPurchaseDbContext.Functions
                .Where(f => f.FunctionDate >= fromDate)
                .OrderBy(f => f.FunctionDate)
                .ToListAsync();
        }
    }
}

