using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.DTOs;

namespace App.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto user);
        Task<bool> UpdateAsync(int id, UserDto user);
        Task<bool> DeleteAsync(int id);

        // 🔐 Login ekleniyor
        Task<UserDto?> LoginAsync(LoginDto dto);
    }
}

