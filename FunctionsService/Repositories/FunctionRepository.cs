using TicketEase.Service.Functions.DbContexts;
using TicketEase.Service.Functions.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketEase.Service.Functions.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly FunctionsDbContext _context;

        public FunctionRepository(FunctionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieFunction>> GetAllFunctionsAsync()
        {
            return await _context.MovieFunctions.ToListAsync();
        }

        public async Task<MovieFunction> GetFunctionByIdAsync(Guid id)
        {
            return await _context.MovieFunctions.FindAsync(id);
        }

        public async Task AddFunctionAsync(MovieFunction function)
        {
            _context.MovieFunctions.Add(function);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFunctionAsync(MovieFunction function)
        {
            _context.MovieFunctions.Update(function);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFunctionAsync(Guid id)
        {
            var function = await _context.MovieFunctions.FindAsync(id);
            if (function != null)
            {
                _context.MovieFunctions.Remove(function);
                await _context.SaveChangesAsync();
            }
        }
    }
}
