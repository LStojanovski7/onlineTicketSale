using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Helpers;
using Data;
using Entities;
using Services.interfaces;

namespace Services.implementation
{
    public class CheckoutServices : ICheckoutServices
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderServices _orderServices;
        private readonly IOrderItemsServices _orderItemsServices;

        public CheckoutServices(AppDbContext context, IHttpContextAccessor httpContext
                                ,UserManager<ApplicationUser> userManager
                                ,IOrderServices orderServices
                                ,IOrderItemsServices orderItemsServices)
        {
            _context = context;
            _httpContext = httpContext;
            _userManager = userManager;
            _orderServices = orderServices;
            _orderItemsServices = orderItemsServices;
        }
        public async Task Checkout()
        {
            List<Tickets> tickets = SessionHelper.GetObjectAsJson<List<Tickets>>(_httpContext.HttpContext.Session, "cart");

            if (tickets == null)
            {
                throw new EmptyCartException("No items in cart, sessionID: " + _httpContext.HttpContext.Session.Id.ToString());
            }

            ApplicationUser user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            Orders order = new Orders
            {
                UserID = user.Id,
                Total = tickets.Sum(t => t.Price),
                DateOfOrder = DateTime.Now.Date
            };


            _orderServices.Add(order);

            foreach (var ticket in tickets)
            {
                OrderItems orderItems = new OrderItems
                {
                    OrderID = order.OrderID,
                    TicketID = ticket.TicketID
                };

                await _orderItemsServices.AddAsync(orderItems);
            }
        }
    }
}
