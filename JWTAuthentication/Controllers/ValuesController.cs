using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUserService UserSvc { get; }

        public ValuesController(IUserService _usr)
        {
            UserSvc = _usr;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await UserSvc.GetCurrentUserAsync();

            var retVal = new
            {
                firmId = user.FirmID,
                userId = user.UserID,
                accessCode = user.AccessCode
            };

            return Ok(retVal);
        }

     
    }
}
