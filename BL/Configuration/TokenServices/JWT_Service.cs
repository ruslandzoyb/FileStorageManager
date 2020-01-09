using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BL.Configuration.TokenServices
{
   public class JWT_Service
    {
      

        static public string GetToken(ClaimsIdentity claims)
        {
            if (claims !=null )
            {
                var tokendesc = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(AuthOptions.LIFETIME),
                    SigningCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)


                };
                var tokenHandler = new JwtSecurityTokenHandler();

                var securityToken = tokenHandler.CreateToken(tokendesc);

                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
            {
                throw new Exception();//todo :ex 
            }

           
    }
    }
}
