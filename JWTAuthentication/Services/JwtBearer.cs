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
        private IUserService _svc;

        private JwtBearer(IUserService svc)
        {
            RequireHttpsMetadata = false;
            SaveToken = true;
            TokenValidationParameters = GetValidationParameters();
            Events = GetEvents();
        }

        private static JwtBearerEvents GetEvents()
        {
            JwtBearerEvents events = new JwtBearerEvents();
            events.OnTokenValidated = OnTokenValidated;
            return events;
        }

        private static Task OnTokenValidated(TokenValidatedContext arg)
        {
            throw new NotImplementedException();
        }


        private static TokenValidationParameters GetValidationParameters()
        {
            TokenValidationParameters param = new TokenValidationParameters();

            param.ValidateIssuerSigningKey = true;
            param.RequireSignedTokens = true;
            param.IssuerSigningKey = new X509SecurityKey(CertificateUtilities.GetCertificate("cb_public.cer"));
            param.ValidateIssuer = false;
            param.ValidateAudience = false;

            return param;
        }


    }
}
