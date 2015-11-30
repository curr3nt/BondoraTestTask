using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.CustomExceptions
{
    public class BackendUnknownResponseException : ABackendException
    {
        public BackendUnknownResponseException(object message) 
            : base("Unknown response format from backend service, please contact web administrator")
        {
            
        }
    }
}