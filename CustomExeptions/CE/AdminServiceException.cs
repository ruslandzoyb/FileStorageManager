using System;
using System.Collections.Generic;
using System.Text;

namespace Exeptions.CE
{
   public class AdminServiceException:Exception
    {

        public AdminServiceException(string message):base(message)
        {

        }
    }
}
