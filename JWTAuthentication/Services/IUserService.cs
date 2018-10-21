using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Models;

namespace JWTAuthentication.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetCurrentUserAsync();
    }
}
