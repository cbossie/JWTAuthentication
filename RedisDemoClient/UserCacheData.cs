using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDemoClient
{
    public class UserCacheData
    {
        public int UserId { get; set; }
        public string AccessKey { get; set; }
        public int FirmId { get; set; }
        public string FavoritePet { get; set; }
        public Dictionary<string, string> UserAttributes = new Dictionary<string, string>();
    }
}
