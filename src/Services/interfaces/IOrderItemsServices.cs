using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Services.interfaces
{
    public interface IOrderItemsServices
    {
        void Add(OrderItems orderItem);
        Task AddAsync(OrderItems orderItem);
    }
}