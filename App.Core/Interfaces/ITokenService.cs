using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Data.Entities;




namespace App.Core.Interfaces
{
    public interface ITokenService
    {
        
        string GenerateToken(User user);

    }
}


