using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Services
{
    public interface IUserService
    {
        string Info { get; }
        void SetInfo(string val);
    }
}
