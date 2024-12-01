namespace PurchaseGallery.Api.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic? Data { get; set; }

        public ApiResponse(int statusCode, string message, dynamic? data = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Data = data;
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Request successful",
                201 => "Resource created",
                204 => "No content",
                400 => "Bad request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Resource not found",
                500 => "Internal server error",
                502 => "Bad gateway",
                503 => "Service unavailable",
                _ => "Unknown status code"
            };
        }
    }
}