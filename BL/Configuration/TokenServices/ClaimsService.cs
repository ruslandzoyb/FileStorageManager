using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BL.Configuration.TokenServices
{
   public class ClaimsService
    {
        public ClaimsIdentity GetClaims(ApplicationUser user,IList<string> roles )
        {

            if (user!=null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Id),
                    
                    

                };
                if (roles!=null)
                {
                    IdentityOptions _options = new IdentityOptions();
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(_options.ClaimsIdentity.RoleClaimType, item));
                    }
                    ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    _options.ClaimsIdentity.RoleClaimType);
                    return claimsIdentity;
                }
                else
                {
                    throw new Exception();// todo :ex
                }

                
                
            }
            else
            {
                throw new Exception();
                //todo :ex
            }
           
        }
    }
}
