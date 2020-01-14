using System;
using System.Collections.Generic;
using System.Text;

namespace Exeptions.CE
{
   public class UserServiceException:Exception
    {
        public UserServiceException(string message):base(message)
        {

        }
    }
}
