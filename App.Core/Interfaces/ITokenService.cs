using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.DTOs;


namespace App.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserDto user);
    }
}

