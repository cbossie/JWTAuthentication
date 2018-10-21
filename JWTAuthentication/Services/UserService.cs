using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Http;
using RedisDemoClient;
using StackExchange.Redis.Extensions.Newtonsoft;

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
            string code = HttpContext.HttpContext.User?.FindFirstValue(Constants.ClaimTypes.AccessClaim);
            var userClaim = int.Parse(HttpContext.HttpContext.User.FindFirstValue(ClaimTypes.Sid));
            var firmClaim = int.Parse(HttpContext.HttpContext.User.FindFirstValue(Constants.ClaimTypes.FirmClaim));


            //Do the redis thing
            var config = RedisConfiguration.CreateFromString(Environment.GetEnvironmentVariable("XCM_REDIS_CONN"));
            using (var cli = new RedisClient(config, new NewtonsoftSerializer()))
            {

                var user = await cli.GetObjectAsync<UserInfo>(code);
                if (user != null)
                {
                    _currUser = user;
                    _currUser.FirmIdFromClaim = firmClaim;
                    _currUser.UserIdFromClaim = userClaim;
                }
                else
                {
                    _currUser = new UserInfo();
                }
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
