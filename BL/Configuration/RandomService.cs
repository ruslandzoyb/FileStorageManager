using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Configuration
{
   public class RandomService
    {
        public static string Random()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(8, true));
            
            builder.Append(RandomString(8, false));
            return builder.ToString();
        }
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }

    

    
}
