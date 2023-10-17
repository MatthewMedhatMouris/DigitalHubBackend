using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Response
{
    public interface IResponseModel
    {
        ResponseModel Response(int statusCode, bool isError, string Message, dynamic Data);
    }


    public class ResponseModel : IResponseModel
    {

        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }


        ResponseModel IResponseModel.Response(int statusCode, bool isError, string Message, dynamic Data)
        {
            return new ResponseModel { Timestamp = DateTime.UtcNow, StatusCode = statusCode, IsError = isError, Message = Message, Result = Data };
        }
    }
}
