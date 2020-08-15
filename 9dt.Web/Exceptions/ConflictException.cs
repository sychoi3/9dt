using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Exceptions
{
    public class ConflictException : ApplicationException
    {
        public ConflictException(): base("The request is a conflict.") { }
        public ConflictException(string message) : base(message) { }
    }
}
