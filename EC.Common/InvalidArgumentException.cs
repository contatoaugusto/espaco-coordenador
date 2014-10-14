using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EC.Common
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string argument) :
            base(string.Concat("Invalid argument ", argument))
        {
        }
    }
}
