using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public object ErrorData { get; private set; }
        public BadRequestException(): base("Invalid request.") { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, object data) : base(message)
        {
            ErrorData = data;
        }
    }
}
