using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.CustomExceptions
{
    public class BackendUnexpectedResponseDataException : ABackendException
    {
        public BackendUnexpectedResponseDataException()
            : base("Unknown response data from server, please contact web administrator")
        {
            
        }
    }
}