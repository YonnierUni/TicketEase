﻿using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;

namespace TicketEase.Service.TicketPurchase.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(Guid ticketId);
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(Guid ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByFunctionAsync(Guid functionId);
        Task<IEnumerable<Ticket>> GetTicketsByUserAsync(string userName);
    }
}
