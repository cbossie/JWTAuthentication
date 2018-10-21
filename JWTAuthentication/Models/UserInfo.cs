using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisDemoClient;

namespace JWTAuthentication.Models
{
    public class UserInfo : UserCacheData
    {
       public int UserIdFromClaim { get; set; }
        public int FirmIdFromClaim { get; set; }
       
    }
}
