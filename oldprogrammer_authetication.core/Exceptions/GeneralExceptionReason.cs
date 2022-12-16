using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Exceptions
{
    public class GeneralExceptionReason
    {
        public GeneralExceptionCode Code { get; set; }
        public string MessageReason { get; set; } = string.Empty;
    }
}
