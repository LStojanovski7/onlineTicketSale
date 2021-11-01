using System.Threading.Tasks;
using Services.interfaces;
using Data;
using Entities;


namespace Services.implementation
{
    public class OrderItemsServices : IOrderItemsServices
    {
        private readonly AppDbContext _context;

        public OrderItemsServices(AppDbContext context)
        {
            _context = context;
        }
        public void Add(OrderItems orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }

        public async Task AddAsync(OrderItems orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}