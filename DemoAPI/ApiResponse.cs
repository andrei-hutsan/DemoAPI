using ServiceReference;

namespace DemoAPI
{
    namespace DemoAPI.Models
    {
        public class ApiResponse<T>
        {
            public bool Success { get; }
            public string Message { get; }
            public T Data { get; }
            public List<string> Errors { get; }

            private ApiResponse(bool success, string message, T data = default, List<string> errors = null)
            {
                Success = success;
                Message = message;
                Data = data;
                Errors = errors ?? new List<string>();
            }

            public static ApiResponse<T> Succeed(T data, string message = "Request successful.")
            {
                return new ApiResponse<T>(true, message, data);
            }

            public static ApiResponse<T> Succeed(string message = "Request successful.")
            {
                return new ApiResponse<T>(true, message);
            }

            public static ApiResponse<T> Fail(string message = "Request failed.", List<string> errors = null)
            {
                return new ApiResponse<T>(false, message, default, errors);
            }
        }
    }
}
