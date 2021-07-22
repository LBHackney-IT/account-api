namespace AccountApi.V1.Infrastructure
{
    public class BaseErrorResponse
    {
        public BaseErrorResponse() { }

        public BaseErrorResponse(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details ?? string.Empty;
        }

        /// <summary>
        /// Status code
        /// </summary>
        /// <example>
        /// 400
        /// </example>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        /// <example>
        /// Model cannot be null
        /// </example>>
        public string Message { get; set; }

        /// <summary>
        /// Stack Trace of Exception
        /// </summary>
        /// <example>
        /// at ChargeApi.V1.UseCase.AddChargeUseCase.ExecuteAsync(AddChargeRequest charge)
        /// </example>
        public string Details { get; set; }
    }
}
