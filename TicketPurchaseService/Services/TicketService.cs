using MassTransit;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Repositories;
using TicketEase.Service.TicketPurchase.Models;
using TicketEase.Service.TicketPurchase.Services.Managers;

using TicketEase.Common.Events;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketEase.Service.TicketPurchase.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IFunctionRepository _functionRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly SeatAvailabilityManager _seatAvailabilityManager;

        public TicketService(ITicketRepository ticketRepository, IFunctionRepository functionRepository, IPublishEndpoint publishEndpoint, SeatAvailabilityManager seatAvailabilityManager)
        {
            _ticketRepository = ticketRepository;
            _functionRepository = functionRepository;
            _publishEndpoint = publishEndpoint;
            _seatAvailabilityManager = seatAvailabilityManager;

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

        public async Task<Ticket> AddTicketAsync(Ticket ticket)
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

            var newTicket = new Ticket
            {
                FunctionId = ticket.FunctionId,
                AdditionalPrice = ticket.AdditionalPrice,
                UserName = ticket.UserName
            };

            await _ticketRepository.AddAsync(newTicket);

            /* Crear el evento con los datos correspondientes
            var ticketAddedEvent = new TicketAddedEvent(
                newTicket.TicketId,
                newTicket.FunctionId,
                newTicket.AdditionalPrice,
                newTicket.UserName
            );

            await _publishEndpoint.Publish(ticketAddedEvent);
            */
            return newTicket;
        }
        public async Task AddMultipleTicketsAsync(IEnumerable<TicketForCreationDto> tickets)
        {
            List<TicketItem> ticketItems = new List<TicketItem>();

            var availableSeats = await _seatAvailabilityManager.CheckSeatsAvailabilityAsync(tickets.FirstOrDefault().FunctionId, tickets.Count());

            if (!availableSeats)
            {
                throw new InvalidOperationException("Not enough seats available for the requested tickets.");
            }

            foreach (var ticket in tickets)
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

                var newTicket = new Ticket
                {
                    FunctionId = ticket.FunctionId,
                    AdditionalPrice = ticket.AdditionalPrice,
                    UserName = ticket.UserName
                };

                await _ticketRepository.AddAsync(newTicket);

                // Crear un objeto TicketItem para agregar a la lista
                ticketItems.Add(new TicketItem(
                    newTicket.TicketId,
                    newTicket.FunctionId,
                    newTicket.AdditionalPrice,
                    newTicket.UserName
                ));
            }

            // Publicar el evento TicketsPurchasedEvent con los datos de todos los tickets agregados
            var ticketsPurchasedEvent = new TicketsPurchasedEvent(ticketItems);
            await _publishEndpoint.Publish(ticketsPurchasedEvent);
        }
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            var existingTicket = await _ticketRepository.GetByIdAsync(ticket.TicketId);
            if (existingTicket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticket.TicketId} does not exist.");
            }

            await _ticketRepository.UpdateAsync(ticket);
            /*
            var TicketUpdated = new TicketUpdatedEvent(
                ticket.TicketId,
                ticket.FunctionId,
                ticket.AdditionalPrice,
                ticket.UserName
            );
            await _publishEndpoint.Publish(TicketUpdated);
            */
        }

        public async Task DeleteTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticketId} not found.");
            }

            await _ticketRepository.DeleteAsync(ticketId);
            //await _publishEndpoint.Publish(new TicketDeletedEvent(ticketId));
        }
        public async Task DeleteMultipleTicketsAsync(IEnumerable<Guid> ticketIds)
        {
            List<TicketItem> ticketItems = new List<TicketItem>();

            foreach (var ticketId in ticketIds)
            {
                var ticket = await _ticketRepository.GetByIdAsync(ticketId);
                if (ticket == null)
                {
                    throw new ArgumentException($"Ticket with ID {ticketId} not found.");
                }

                await _ticketRepository.DeleteAsync(ticketId);

                // Crear un objeto TicketItem para agregar a la lista
                ticketItems.Add(new TicketItem(
                    ticket.TicketId,
                    ticket.FunctionId,
                    ticket.AdditionalPrice,
                    ticket.UserName
                ));
            }

            // Publicar el evento TicketCancelledEvent con los tickets eliminados
            var ticketsCancelledEvent = new TicketsCancelledEvent(ticketItems);
            await _publishEndpoint.Publish(ticketsCancelledEvent);
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
