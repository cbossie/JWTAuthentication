using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Http;

namespace JWTAuthentication.Services
{
    public static class IHttpContextAccessorExtension
    {
        public static async Task<UserInfo> CurrentUser(
            this IHttpContextAccessor httpContextAccessor,
            UserService users
        )
        {
            return null;
        }
    }
}
