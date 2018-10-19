using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace JWT_Generation
{
    public class JwtHelper
    {


        public static string GetJwt(X509Certificate2 cert, string accessCode)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            var securityKey = new X509SecurityKey(cert);
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, "95"),
                new Claim("xcm:graphqlaccesstoken", accessCode)

                
            }, "Custom");



            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred,
                Subject = claimsIdentity
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = (JwtSecurityToken)tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            

            return signedAndEncodedToken;
        }




    }
}
