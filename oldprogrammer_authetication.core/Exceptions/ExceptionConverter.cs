using oldprogrammer_authetication.core.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Exceptions
{
    public static class ExceptionConverter
    {
        public static GeneralException ToGeneralException(this Exception ex)
        {
            return new GeneralException(ex.Message);
        }
        public static GeneralExceptionResponse ToGeneralExceptionResponse(this Exception ex)
        {
            return new GeneralExceptionResponse(ex.Message);
        }
        public static GeneralExceptionResponse ToGeneralExceptionResponse(this GeneralException ge)
        {
            return new GeneralExceptionResponse { Code = ge.Code.ToString(), MessageReason = ge.MessageReason };
        }
    }
}
