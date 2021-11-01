using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.interfaces
{
    public interface ITicketServices
    {
        void CreateTicket(Tickets ticket);
        Task CreateTicketAsync(Tickets ticket);
        Task UpdateExistingAsync(Tickets ticket);
        Task DeleteTicketAsync(int id);
        Tickets GetTicketDetails(int? id);
        Task<Tickets> GetTicketDetailsAsync(int? id);
        List<Tickets> GetAllTickets();
        Task<List<Tickets>> GetAllTicketsAsync();
    }
}