using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Outputs
{
    public class GeneralExceptionResponse
    {
        public GeneralExceptionResponse() { }
        public GeneralExceptionResponse(string message)
        {
            MessageReason = message;
        }
        public GeneralExceptionResponse(string messageReason, string code) : this(messageReason)
        {
            Code = code;
        }

        public string MessageReason { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
