using System.Net;

namespace NZWalks.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Proceed to the next middleware
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                try
                {
                    // Log the exception details
                    logger.LogError(ex, $"{errorId} : {ex.Message}");
                }
                catch (Exception logEx)
                {
                    // Handle logging failure (in case the logging itself fails)
                    Console.WriteLine("Logging failed: " + logEx.Message);
                }

                // Return a custom error response
                if (!httpContext.Response.HasStarted)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    var error = new
                    {
                        Id = errorId,
                        ErrorMessage = "Something went wrong, we are looking to resolve this",
                    };

                    // Safely write the error response
                    await httpContext.Response.WriteAsJsonAsync(error);
                }
            }
        }
    }
}
