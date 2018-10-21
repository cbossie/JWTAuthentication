using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RedisDemoClient;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace JWT_Generation
{
    class Program
    {
        static void Main(string[] args)
        {
            var passkey = args[1];
            var certificateFile = args[0];

            SecureString passKeySecure = new SecureString();
            passkey.ToList().ForEach(a => passKeySecure.AppendChar(a));


            var cert = CertificateHelper.GetCertificate(certificateFile, passKeySecure);

           bool cn = true;

            while (cn)
            {
                Console.WriteLine("Press any key to Generate JWT - except 'x'. Press x to E(x)it");
                var x = Console.ReadKey();
                if (x.KeyChar == 'x')
                {
                    cn = false;
                    continue;
                }

                Generate(cert);

            }

            Console.WriteLine($"Goodbye");
            Console.ReadKey();



        }


        public static  void Generate(X509Certificate2 cert)
        {
            Console.WriteLine($"----------JWT BELOW-----------");

            var userData = new UserCacheData()
            {
                AccessKey = Guid.NewGuid().ToString(),
                FirmId = DateTime.Now.Millisecond,
                UserId = DateTime.Now.Millisecond + 19,
                FavoritePet = DateTime.Now.Millisecond % 2 == 0 ? "Cat" : "Hamster",
            };
            userData.UserAttributes["Attribute1"] = "CatName";
            userData.UserAttributes["Attribute2"] = "DocName";
            userData.UserAttributes["Attribute3"] = "IceCreamFlavor";

            var jwt = JwtHelper.GetJwt(cert, userData.AccessKey, userData.UserId, userData.FirmId);

            var config = RedisConfiguration.CreateFromString(ConfigurationManager.AppSettings["RedisConnString"]);
            using (var cli = new RedisClient(config, new NewtonsoftSerializer()))
            {
                cli.AddObject(userData.AccessKey, userData);
            }


            Console.WriteLine(jwt);
            Console.WriteLine($"----------END JWT-----------");
        }


    }
}
