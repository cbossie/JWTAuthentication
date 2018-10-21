using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Http;

namespace JWTAuthentication.Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor HttpContext { get; }
        private UserInfo _currUser;

        public UserService(IHttpContextAccessor context)
        {
            HttpContext = context;
        }

        private async Task PopulateCurrentUser()
        {
            _currUser = new UserInfo()
            {
                AccessCode = HttpContext.HttpContext.User?.FindFirstValue(Constants.ClaimTypes.AccessClaim)
            };

            var userClaim = HttpContext.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            if (int.TryParse(userClaim, out int userID))
            {
                _currUser.UserID = userID;
            }

            var firmClaim = HttpContext.HttpContext.User.FindFirstValue(Constants.ClaimTypes.FirmClaim);
            if (int.TryParse(firmClaim, out int firmId))
            {
                _currUser.FirmID = firmId;
            }
        }

        public async Task<UserInfo> GetCurrentUserAsync()
        {
            if (_currUser == null)
            {
                await PopulateCurrentUser();
            }

            return _currUser;
        }


    }
}
