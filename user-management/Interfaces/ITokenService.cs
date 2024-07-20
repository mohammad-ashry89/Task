using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_management.Models;

namespace user_management.Interfaces
{
    public interface ITokenService
    {
        Task<string> createToken(User user);
    }
}