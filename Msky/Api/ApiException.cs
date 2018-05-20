using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace Msky.Api
{
    public class ApiException : Exception
    {
        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
