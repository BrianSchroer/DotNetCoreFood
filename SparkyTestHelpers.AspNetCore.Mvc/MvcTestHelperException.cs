using System;

namespace SparkyTestHelpers.AspNetCore.Mvc
{
    public class MvcTestHelperException : Exception
    {
        public MvcTestHelperException() : base()
        {
        }

        public MvcTestHelperException(string message)
            : base(message)
        {
        }

        public MvcTestHelperException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MvcTestHelperException(
            System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
