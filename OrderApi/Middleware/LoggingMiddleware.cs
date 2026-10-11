using Microsoft.AspNetCore.Mvc;
namespace OrderApi.Middleware
{
    public class LoggingMiddleware
    {

        private readonly RequestDelegate _next; 
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync
            (HttpContext context)
        {

            _logger.LogInformation(
                "Request {Method} {Path} TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.TraceIdentifier);
            await _next(context);
            _logger.LogInformation(
                "Response {StatusCode} TraceId: {TraceId}",
                context.Response.StatusCode,
                context.TraceIdentifier);
        }

    }
}
