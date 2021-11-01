using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.interfaces
{
    public interface ICartServices
    {
        List<Tickets> GetCart();
        void AddToCart(Tickets ticket);
        void Remove(Tickets ticket);
        // Task PlaceOrder();
        bool ExistInSession(int? id);
    }
}