using System;
using System.Collections.Generic;
using System.Text;

namespace Exeptions.CE
{
  public  class AccountException: Exception
    {

        public AccountException(string message):base(message)
        {

        }
    }
}
