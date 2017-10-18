using System;
using System.Runtime.Serialization;

namespace OpenEngine
{
    public class ComponentNotFoundException : Exception
    {
        public ComponentNotFoundException()
        {
        }
        public ComponentNotFoundException(string message) : base(message)
        {
        }
        public ComponentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected ComponentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
