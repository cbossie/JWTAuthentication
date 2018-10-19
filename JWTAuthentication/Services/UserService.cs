using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Services
{
    public class UserService : IUserService
    {
        public string Info { get; private set; }
        public UserService()
        {
            
        }

        public void SetInfo(string val)
        {
            Info = val;
        }
    }
}
