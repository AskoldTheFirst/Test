using System.Text.Json;
using Microsoft.AspNetCore.Mvc;


namespace API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly LogClient.ILogger _logger;

        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
                                   LogClient.ILogger logger,
                                   IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("InvokeAsync", LogClient.Types.Severity.High, ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = new ProblemDetails
                {
                    Status = 500,
                    Detail = _env.IsDevelopment() ? ex.StackTrace.ToString() : null,
                    Title = ex.Message
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}