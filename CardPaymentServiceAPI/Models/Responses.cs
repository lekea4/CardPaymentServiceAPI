using Newtonsoft.Json;

namespace CardPaymentServiceAPI.Models
{
    public class Responses <T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Errors { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }

        public Responses(int statusCode, bool success, string msg, T data, string errors, string responseMessage, string responseCode)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
            Errors = errors;
            ResponseMessage = responseMessage;
            ResponseCode = responseCode;
        }
        public Responses()
        {
        }

        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Responses<T> Fail(string responseMessage, string responseCode, string errorMessage, int statusCode = 404)
        {
            return new Responses<T> { ResponseCode = responseCode, ResponseMessage = responseMessage, Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }

        public static Responses<T> Fail(string responseMessage, string responseCode, int statusCode = 404)
        {
            return new Responses<T> { ResponseCode = responseCode, ResponseMessage = responseMessage, Succeeded = false, StatusCode = statusCode };
        }
        public static Responses<T> Success(string responseMessage, string responseCode, string successMessage, T data, int statusCode = 200)
        {
            return new Responses<T> { ResponseCode = responseCode, ResponseMessage = responseMessage, Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public static Responses<T> Success(string responseMessage, string responseCode, T data, int statusCode = 200)
        {
            return new Responses<T> { ResponseCode = responseCode, ResponseMessage = responseMessage, Succeeded = true, Data = data, StatusCode = statusCode };
        }

        public static Responses<T> Success(string responseMessage, string responseCode, int statusCode = 200)
        {
            return new Responses<T> { ResponseCode = responseCode, ResponseMessage = responseMessage, Succeeded = true, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
