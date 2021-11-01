using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.interfaces
{
    public interface IOrderServices
    {
        ICollection<Orders> GetAll();
        Task<ICollection<Orders>> GetAllAsync();
        // Orders GetOrderItems(int id);
        // Task<Orders> GetOrderItemsAsync();
        void Add(Orders order);
        Task AddAsync(Orders order);
        
        // Task DeleteOrderAsync(int id);
        // void UpdateOrder(int id);
        // Task UpdateOrderAsync(int id);
    }
}