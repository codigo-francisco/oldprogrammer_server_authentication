using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Exceptions
{
    public class GeneralException : Exception
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GeneralExceptionCode Code { get; set; }
        public string MessageReason { get; set; }
        public GeneralException(string message) : base(message)
        {
            if (message == "InvalidUserName")
            {
                Code = GeneralExceptionCode.InvalidUsername;
                MessageReason = message;
            }
            else if (message == "UserNotFound")
            {
                Code = GeneralExceptionCode.UserNotFound;
                MessageReason = message;
            }
            else
            {
                Code = GeneralExceptionCode.Unknown;
                MessageReason = message;
            }
        }
        public GeneralException(string message, GeneralExceptionCode code)
            : this(message)
        {
            Code = code;
        }
    }
}
