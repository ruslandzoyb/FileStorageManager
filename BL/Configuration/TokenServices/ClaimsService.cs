using DAL.Models.IdentityModels;
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
                    
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, item));
                    }
                    ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
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
