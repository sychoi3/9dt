using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Exceptions
{
    public class GoneException : ApplicationException
    {
        public GoneException(): base("The requested resource is gone.") { }
        public GoneException(string message) : base(message) { }
    }
}
