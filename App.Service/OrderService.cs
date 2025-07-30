using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Where(o => o.Enabled)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Notes = o.Notes,
                    IsPaid = o.IsPaid,
                    Enabled = o.Enabled,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null || !order.Enabled)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Notes = order.Notes,
                IsPaid = order.IsPaid,
                Enabled = order.Enabled,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            // İlgili ürün bilgilerini getir
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null || !product.Enabled)
                throw new Exception("Ürün bulunamadı veya pasif.");

            // Toplam tutarı hesapla
            var total = product.Price * dto.Quantity;

            var order = new Order
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                OrderDate = DateTime.UtcNow,
                TotalAmount = total,
                Notes = dto.Notes,
                IsPaid = dto.IsPaid,
                Enabled = true
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            dto.Id = order.Id;
            dto.TotalAmount = total;
            dto.OrderDate = order.OrderDate;
            dto.CreatedAt = order.CreatedAt;

            return dto;
        }
        

        public async Task<bool> UpdateAsync(int id, OrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null || !order.Enabled)
                return false;

            order.UserId = dto.UserId;
            order.OrderDate = dto.OrderDate;
            order.TotalAmount = dto.TotalAmount;
            order.Notes = dto.Notes;
            order.IsPaid = dto.IsPaid;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null || !order.Enabled)
                return false;

            order.Enabled = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
