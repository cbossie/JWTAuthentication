using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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


            var jwt = JwtHelper.GetJwt(cert, "ABCDEFG", 98, 444);


            Console.WriteLine(jwt);
            Console.ReadKey();



        }
    }
}
