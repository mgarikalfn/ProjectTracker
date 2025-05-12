using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Domain.Exception
{
    public class DomainException : System.Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, System.Exception inner)
            : base(message, inner) { }

        // For serialization support if needed
        protected DomainException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
