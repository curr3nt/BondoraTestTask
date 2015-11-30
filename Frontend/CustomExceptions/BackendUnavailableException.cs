using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.CustomExceptions
{
    public class BackendUnavailableException : ABackendException
    {
        public BackendUnavailableException(Exception exception)
            : base("Cannot reach backend service, please try your request later", exception)
        {
            
        }
    }
}