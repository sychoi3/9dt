using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(): base("The requested resource was not found.") { }
        public NotFoundException(string message) : base(message) { }
    }
}
