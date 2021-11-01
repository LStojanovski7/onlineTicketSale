using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Services.interfaces;

namespace App.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketServices _ticketServices;
        public TicketsController(ITicketServices ticketServices)
        {
            _ticketServices = ticketServices;
        }

        // GET: Tickets
        [Authorize(Roles = "admin,standard")]
        public async Task<IActionResult> Index()
        {
            List<Tickets> tickets = await _ticketServices.GetAllTicketsAsync();

            return View(tickets);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketServices.GetTicketDetailsAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }  

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("TicketID,Name,Price,Date")] Tickets ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketServices.CreateTicketAsync(ticket);

                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketServices.GetTicketDetailsAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }
            
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,Name,Price,Date")] Tickets ticket)
        {
            if (id != ticket.TicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketServices.UpdateExistingAsync(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketServices.GetTicketDetailsAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketServices.DeleteTicketAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TicketExists(int id)
        {
            var tickets = await _ticketServices.GetAllTicketsAsync();

            return tickets.Any(x => x.TicketID == id);
        }
    }
}