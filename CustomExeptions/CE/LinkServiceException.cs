using System;
using System.Collections.Generic;
using System.Text;

namespace Exeptions.CE
{
   public class LinkServiceException:Exception
    {
        public LinkServiceException(string message):base(message)
        {

        }
    }
}
