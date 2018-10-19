using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Generation
{
    public static class CertificateHelper
    {
        public static X509Certificate2 GetCertificate(string certificateFileName, SecureString passKey )
        {
            var cert = new X509Certificate2();
            cert.Import(certificateFileName, passKey, X509KeyStorageFlags.PersistKeySet);


            return cert;
        }


    }
}
