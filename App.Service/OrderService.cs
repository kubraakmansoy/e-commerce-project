
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Core.Interfaces;
using App.Data.Contexts;
using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            return await _context.Orders
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderCode = o.OrderCode,
                    Address = o.Address,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderCode = order.OrderCode,
                Address = order.Address,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderCode = dto.OrderCode,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            dto.Id = order.Id;
            dto.CreatedAt = order.CreatedAt;

            return dto;
        }

        public async Task<bool> UpdateAsync(int id, OrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            order.UserId = dto.UserId;
            order.OrderCode = dto.OrderCode;
            order.Address = dto.Address;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

