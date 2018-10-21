using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public static class Constants
    {
        public static class ClaimTypes
        {
            public static readonly string FirmClaim = "cb:firmid";
            public static readonly string AccessClaim = "cb:accesscode";
        }
    }
}
