using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App.Core.DTOs;

namespace App.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(OrderDto dto);
        Task<bool> UpdateAsync(int id, OrderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

