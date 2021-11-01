using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities;
using Services.interfaces;
using Data;

namespace Services.implementation
{
    public class TicketServices : ITicketServices
    {
        private readonly AppDbContext _context;

        public TicketServices(AppDbContext context)
        {
            _context = context;
        }
        public void CreateTicket(Tickets ticket)
        {
            if (Exists(ticket.TicketID))
            {
                UpdateExisting(ticket);
                return;
            }

            try
            {
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task CreateTicketAsync(Tickets ticket)
        {
            if (Exists(ticket.TicketID))
            {
                await UpdateExistingAsync(ticket);
                return;
            }

            try
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket != null)
            {
                try
                {
                    _context.Tickets.Remove(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            else
            {
                throw (new TicketNotFoundException("Ticket not found"));
            }
        }

        public Tickets GetTicketDetails(int? id)
        {
            var ticket = _context.Tickets.FirstOrDefault<Tickets>(ob => ob.TicketID == id);

            if (ticket == null)
            {
                throw (new TicketNotFoundException("Ticket not found"));
            }

            return ticket;
        }
        public async Task<Tickets> GetTicketDetailsAsync(int? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync<Tickets>(ob => ob.TicketID == id);

            if (ticket == null)
            {
                throw (new TicketNotFoundException("Ticket not found"));
            }

            return ticket;
        }
 
        public List<Tickets> GetAllTickets() => _context.Tickets.ToList<Tickets>();

        public async Task<List<Tickets>> GetAllTicketsAsync() => await _context.Tickets.ToListAsync<Tickets>();

        public void UpdateExisting(Tickets ticket)
        {
            try
            {
                _context.Tickets.Update(ticket);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw(ex);
            }
        }
        public async Task UpdateExistingAsync(Tickets ticket)
        {
            try
            {
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw(ex);
            }
        }

        public bool Exists(int id)
        {
            var result = _context.Tickets.Find(id);

            if (result == null)
            {
                return false;
            }

            return true;
        }
    }
}