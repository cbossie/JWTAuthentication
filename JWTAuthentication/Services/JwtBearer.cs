using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTUtilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication.Services
{
    public class JwtBearer : JwtBearerOptions
    {
        private JwtBearer()
        {
            RequireHttpsMetadata = false;
            SaveToken = true;
            TokenValidationParameters = GetValidationParameters();
            
            
        }

        

        public static JwtBearer GetBearer => new JwtBearer();


        private static TokenValidationParameters GetValidationParameters()
        {
            TokenValidationParameters param = new TokenValidationParameters();

            param.ValidateIssuerSigningKey = true;
            param.RequireSignedTokens = true;
            param.IssuerSigningKey = new X509SecurityKey(CertificateUtilities.GetCertificate("xcm_cb_public.cer"));
            param.ValidateIssuer = false;
            param.ValidateAudience = false;

            return param;
        }


    }
}
