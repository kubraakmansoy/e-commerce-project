using App.Core.DTOs;
using App.Core.Interfaces;
using App.Data.Contexts;
using App.Data.Entities;
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
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    SellerId = p.SellerId,
                    CategoryId = p.CategoryId,
                    Name = p.Name,
                    Price = p.Price,
                    Details = p.Details,
                    StockAmount = p.StockAmount,
                    Enabled = p.Enabled,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                SellerId = product.SellerId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                Details = product.Details,
                StockAmount = product.StockAmount,
                Enabled = product.Enabled,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var product = new Product
            {
                SellerId = dto.SellerId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Price = dto.Price,
                Details = dto.Details,
                StockAmount = dto.StockAmount,
                Enabled = dto.Enabled,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            dto.Id = product.Id;
            dto.CreatedAt = product.CreatedAt;

            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.SellerId = dto.SellerId;
            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Details = dto.Details;
            product.StockAmount = dto.StockAmount;
            product.Enabled = dto.Enabled;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
