using MassTransit;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Repositories;
using TicketEase.Service.TicketPurchase.Events;
using TicketEase.Service.TicketPurchase.Models;

namespace TicketEase.Service.TicketPurchase.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IFunctionRepository _functionRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public TicketService(ITicketRepository ticketRepository, IFunctionRepository functionRepository, IPublishEndpoint publishEndpoint)
        {
            _ticketRepository = ticketRepository;
            _functionRepository = functionRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticketId} not found.");
            }
            return ticket;
        }

        public async Task AddTicketAsync(TicketForCreationDto ticket)
        {
            var function = await _functionRepository.GetByIdAsync(ticket.FunctionId);
            if (function == null)
            {
                throw new ArgumentException("The function associated with the ticket does not exist.");
            }

            if (string.IsNullOrEmpty(ticket.UserName))
            {
                throw new ArgumentException("User name cannot be empty.");
            }

            await _ticketRepository.AddAsync(ticket);
            await _publishEndpoint.Publish(new TicketAddedEvent(ticket));
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            var existingTicket = await _ticketRepository.GetByIdAsync(ticket.TicketId);
            if (existingTicket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticket.TicketId} does not exist.");
            }

            await _ticketRepository.UpdateAsync(ticket);
            await _publishEndpoint.Publish(new TicketUpdatedEvent(ticket));
        }

        public async Task DeleteTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticketId} not found.");
            }

            await _ticketRepository.DeleteAsync(ticketId);
            await _publishEndpoint.Publish(new TicketDeletedEvent(ticketId));
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByFunctionAsync(Guid functionId)
        {
            return await _ticketRepository.GetTicketsByFunctionIdAsync(functionId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(string userName)
        {
            return await _ticketRepository.GetTicketsByUserNameAsync(userName);
        }
    }
}
