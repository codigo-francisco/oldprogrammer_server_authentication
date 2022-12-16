using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralExceptionReason GeneralReason { get; set; } = new GeneralExceptionReason { Code = GeneralExceptionCode.Unknown, MessageReason = String.Empty };
        public GeneralException(string message) : base(message)
        {

        }
        public GeneralException(string message, GeneralExceptionReason reason)
            : this(message)
        {
            GeneralReason = reason;
        }
    }
}
