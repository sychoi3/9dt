using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _9dt.Web.Exceptions
{
    public interface IErrorResponse
    {
        string Message { get; }
        HttpStatusCode StatusCode { get; }
        object ErrorData { get; }
    }
}
