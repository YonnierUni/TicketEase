using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;

namespace TicketEase.Service.TicketPurchase.Services
{
    public interface IFunctionService
    {
        Task<IEnumerable<FunctionDto>> GetAllFunctionsAsync();
        Task<FunctionDto> GetFunctionByIdAsync(Guid functionId);
        Task<FunctionDto> AddFunctionAsync(FunctionForCreationDto functionForCreationDto);
        Task<FunctionDto> UpdateFunctionAsync(FunctionForUpdateDto ForUpdateDto);
        Task<bool> DeleteFunctionAsync(Guid functionId);
        Task<IEnumerable<FunctionDto>> GetUpcomingFunctionsAsync(DateTime fromDate);
    }
}