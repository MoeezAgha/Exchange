using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.ResponseWrapperService
{
    //public static class ResponseHelper
    //{
    //    public static IActionResult GenerateResponse<T>(T data, int statusCode, string successMessage, string errorMessage = null)
    //    {
    //        if (statusCode >= 200 && statusCode < 300)
    //        {
    //            var response = new ResponseWrapper<T>
    //            {
    //                Success = true,
    //                StatusCode = statusCode,
    //                Message = successMessage,
    //                Data = data
    //            };

    //            return new OkObjectResult(response);
    //        }
    //        else
    //        {
    //            var response = new ResponseWrapper<T>
    //            {
    //                Success = false,
    //                StatusCode = statusCode,
    //                Message = errorMessage,
    //                Data = default
    //            };

    //            return new ObjectResult(response)
    //            {
    //                StatusCode = statusCode
    //            };
    //        }
    //    }
    //}
    public class ResponseWrapper<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class RepositoryResult<T>
    {
        public bool Success { get; set; }
        public T Entity { get; set; }
        public string ErrorMessage { get; set; }
    }

}
