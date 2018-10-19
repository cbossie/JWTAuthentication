using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWTUtilities
{
    public class CertificateUtilities 
    {
        public static X509Certificate2 GetCertificate(string certificateFileName)
        {
            var cert = new X509Certificate2(certificateFileName);
   

            return cert;
        }


    }
}
