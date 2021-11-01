using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services.interfaces;
using Services.Helpers;

namespace App.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderServices _orderServices;
        private readonly ICheckoutServices _checkoutServices;

        public OrdersController(IOrderServices orderServices, UserManager<ApplicationUser> userManager 
                                ,ICheckoutServices checkoutServices)
        {
            _orderServices = orderServices;
            _userManager = userManager;
            _checkoutServices = checkoutServices;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            ICollection<Orders> orders = await _orderServices.GetAllAsync();

            return View(orders);
        } 

     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            await _checkoutServices.Checkout();
            
            return View();
        }

        // GET: Orders/Edit/5 
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var orders = await _context.Orders.FindAsync(id);
        //     if (orders == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "Name", orders.TicketID);
        //     ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", orders.UserID);
        //     return View(orders);
        // }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(string id, [Bind("UserID,TicketID,Total")] Orders orders)
        // {
        //     if (id != orders.UserID)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(orders);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!OrdersExists(orders.UserID))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "Name", orders.TicketID);
        //     ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", orders.UserID);
        //     return View(orders);
        // }

        // GET: Orders/Delete/5
        // public async Task<IActionResult> Delete(string id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var orders = await _context.Orders
        //         .Include(o => o.Ticket)
        //         .Include(o => o.User)
        //         .FirstOrDefaultAsync(m => m.UserID == id);
        //     if (orders == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(orders);
        // }

        // // POST: Orders/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(string id)
        // {
        //     var orders = await _context.Orders.FindAsync(id);
        //     _context.Orders.Remove(orders);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        private bool OrdersExists(string id)
        {
            return _orderServices.GetAll().Any(ob => ob.UserID == id);
        }
    }
}
