using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Services.Helpers;
using Services.interfaces;
using Entities;
using Data;


namespace Services.implementation
{
    public class CartServices : ICartServices
    {
        private readonly ITicketServices _ticketServices;
        private readonly IHttpContextAccessor _httpContext;

        public CartServices(ITicketServices ticketServices, IHttpContextAccessor httpContext)
        {
            _ticketServices = ticketServices;
            _httpContext = httpContext;
        }

        public List<Tickets> GetCart() => SessionHelper.GetObjectAsJson<List<Tickets>>(_httpContext.HttpContext.Session, "cart");
  
        public void AddToCart(Tickets ticket)
        {
            if(SessionHelper.GetObjectAsJson<List<Tickets>>(_httpContext.HttpContext.Session, "cart") == null)
            {
                List<Tickets> sessionCart = new List<Tickets>();

                sessionCart.Add(ticket);

                SessionHelper.SetObjectAsJson(_httpContext.HttpContext.Session, "cart", sessionCart);
            }
            else
            {
                List<Tickets> sessionCart = SessionHelper.GetObjectAsJson<List<Tickets>>(_httpContext.HttpContext.Session, "cart");

                if(!ExistInSession(ticket.TicketID))
                {
                    sessionCart.Add(ticket);
                }

                SessionHelper.SetObjectAsJson(_httpContext.HttpContext.Session, "cart", sessionCart);
            }
        }

        public void Remove(Tickets ticket)
        {
            List<Tickets> sessionCart = SessionHelper.GetObjectAsJson<List<Tickets>>(_httpContext.HttpContext.Session, "cart");

            sessionCart.RemoveAll(ob => ob.TicketID == ticket.TicketID);

            SessionHelper.SetObjectAsJson(_httpContext.HttpContext.Session, "cart", sessionCart);
        }

        public bool ExistInSession(int? id) => SessionHelper.GetObjectAsJson<List<Tickets>>(_httpContext.HttpContext.Session, "cart")
                                                .Where<Tickets>(t => t.TicketID == id)
                                                .FirstOrDefault() == null ? false : true;
    }
}