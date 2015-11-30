using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.CustomExceptions
{
    public abstract class ABackendException : Exception
    {
        protected ABackendException(string message) : base(message)
        {
            
        }

        protected ABackendException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}