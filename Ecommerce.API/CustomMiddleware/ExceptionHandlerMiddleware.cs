using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.CustomMiddleware
{
    public class ExceptionHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next=next;
            _logger=logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                if(httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var Problem = new ProblemDetails()
                    {
                        Title = "Error while processing your HTTP request -End point Not Found-",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"The requested endpoint '{httpContext.Request.Path}' was not found on the server.",
                        Instance = httpContext.Request.Path,
                    };

                    await httpContext.Response.WriteAsJsonAsync(Problem);
                }
            }

            catch (Exception ex)
            {
                // Logging 
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

                // Set the response status code and content type (Return Custom Error Response)

                //change the status code in network response to to 500 Internal Server Error
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var Problem = new ProblemDetails()
                {
                    Title = "An error occurred while processing your request.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                };

                // then return the ProblemDetails as JSON in the response body
                await httpContext.Response.WriteAsJsonAsync(Problem);

            }
        }
    }
}
