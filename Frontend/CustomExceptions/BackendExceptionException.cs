using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.CustomExceptions
{
    public class BackendExceptionException : ABackendException
    {
        public BackendExceptionException()
            : base("An error has occured on the service-side, please try your request later")
        {
            
        }
    }
}