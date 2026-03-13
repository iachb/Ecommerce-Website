using Newtonsoft.Json;

namespace Ecommerce.Api.Errors
{
    public class CodeErrorResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string[]? Message { get; set; }

        public CodeErrorResponse(int statusCode, string[]? message = null)
        {
            StatusCode = statusCode;
            if (message != null)
            {
                Message = message;
            }
            else
            {
                var text = GetDefaultMessageStatusCode(statusCode);
                Message = new string[] { text };
            }
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Error"
            };
        }
    }
}
