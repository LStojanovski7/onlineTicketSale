using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Services.interfaces;
using Data;
using Entities;

namespace Services.implementation
{
    public class OrderServices : IOrderServices
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public OrderServices(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor;
        }

        public ICollection<Orders> GetAll() => _context.Orders.ToList<Orders>();

        public async Task<ICollection<Orders>> GetAllAsync() => await _context.Orders.ToListAsync<Orders>();

        public void Add(Orders order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }

        }
        public async Task AddAsync(Orders order)
        {
            try
            {
                await _context.Orders.AddAsync(order);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);

            try
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

    }
}
