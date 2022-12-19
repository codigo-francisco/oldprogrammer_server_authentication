using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Exceptions
{
    public class GeneralExceptionEmailExists : GeneralException
    {
        private const string EmailMessage = "Email Already Exists";
        public GeneralExceptionEmailExists() : base(EmailMessage)
        {
            Code = GeneralExceptionCode.EmailExists;
            MessageReason = EmailMessage;
        }
    }
}
