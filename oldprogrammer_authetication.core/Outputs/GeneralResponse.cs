using oldprogrammer_authetication.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldprogrammer_authetication.core.Outputs
{
    public class GeneralResponse
    {
        public GeneralResponse() { }
        public GeneralResponse(int? statusCode = null)
        {
            StatusCode = statusCode;
        }

        public List<GeneralExceptionResponse> Errors { get; set; } = new List<GeneralExceptionResponse>();
        public int? StatusCode { get; set; }
    }
    public class GeneralResponse<T>
    {
        public GeneralResponse() { }
        public GeneralResponse(T? data, int? statusCode = null)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public List<GeneralExceptionResponse> Errors { get; set; } = new List<GeneralExceptionResponse>();
        public T? Data { get; set; }
        public int? StatusCode { get; set; }
    }
}
