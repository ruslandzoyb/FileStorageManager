using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Configuration.TokenServices
{
   public class AuthOptions
    {

        public const string ISSUER = "API"; // издатель токена
        public const string AUDIENCE = "Postman"; // потребитель токена
        const string KEY = "most!secretkeyevercreat2ed";   // ключ для шифрации
        public const int LIFETIME = 25; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
