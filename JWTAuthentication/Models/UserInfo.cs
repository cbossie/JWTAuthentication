using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public int? FirmID { get; set; }
        public string AccessCode { get; set; }
        
    }
}
