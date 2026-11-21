using System.Diagnostics;

namespace Project.Middleware;

/// <summary>
/// Logs application HTTP requests without recording query strings or submitted form data.
/// </summary>
public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;
        var path = request.Path.Value ?? "/";

        try
        {
            await _next(context);
            stopwatch.Stop();

            var endpoint = context.GetEndpoint()?.DisplayName ?? "Unmatched endpoint";
            var controller = context.Request.RouteValues.TryGetValue("controller", out var controllerValue)
                ? controllerValue?.ToString()
                : null;
            var action = context.Request.RouteValues.TryGetValue("action", out var actionValue)
                ? actionValue?.ToString()
                : null;
            var operation = !string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action)
                ? $"{controller}.{action}"
                : endpoint;
            var actor = context.User.Identity?.IsAuthenticated == true
                ? context.User.Identity.Name ?? "Authenticated user"
                : "Anonymous";
            var statusCode = context.Response.StatusCode;
            var message = "Operation {Operation} via {Method} {Path} by {Actor} returned {StatusCode} in {ElapsedMilliseconds} ms. TraceId: {TraceId}";

            if (statusCode >= StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(message, operation, request.Method, path, actor, statusCode, stopwatch.ElapsedMilliseconds, context.TraceIdentifier);
            }
            else if (statusCode >= StatusCodes.Status400BadRequest)
            {
                _logger.LogWarning(message, operation, request.Method, path, actor, statusCode, stopwatch.ElapsedMilliseconds, context.TraceIdentifier);
            }
            else
            {
                _logger.LogInformation(message, operation, request.Method, path, actor, statusCode, stopwatch.ElapsedMilliseconds, context.TraceIdentifier);
            }
        }
        catch (Exception exception)
        {
            stopwatch.Stop();
            _logger.LogError(
                exception,
                "Operation {Operation} via {Method} {Path} by {Actor} failed after {ElapsedMilliseconds} ms. TraceId: {TraceId}",
                GetOperation(context),
                request.Method,
                path,
                context.User.Identity?.Name ?? "Anonymous",
                stopwatch.ElapsedMilliseconds,
                context.TraceIdentifier);
            throw;
        }
    }

    private static string GetOperation(HttpContext context)
    {
        var controller = context.Request.RouteValues.TryGetValue("controller", out var controllerValue)
            ? controllerValue?.ToString()
            : null;
        var action = context.Request.RouteValues.TryGetValue("action", out var actionValue)
            ? actionValue?.ToString()
            : null;

        return !string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action)
            ? $"{controller}.{action}"
            : context.GetEndpoint()?.DisplayName ?? "Unmatched endpoint";
    }
}
