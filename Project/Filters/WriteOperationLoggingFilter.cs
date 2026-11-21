using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Filters;

/// <summary>
/// Adds an operation-level record for form and API writes without logging submitted values.
/// </summary>
public sealed class WriteOperationLoggingFilter : IAsyncActionFilter
{
    private readonly ILogger<WriteOperationLoggingFilter> _logger;

    public WriteOperationLoggingFilter(ILogger<WriteOperationLoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var method = context.HttpContext.Request.Method;
        if (HttpMethods.IsGet(method) || HttpMethods.IsHead(method) || HttpMethods.IsOptions(method))
        {
            await next();
            return;
        }

        var action = context.ActionDescriptor as ControllerActionDescriptor;
        var controllerName = action?.ControllerName ?? "UnknownController";
        var actionName = action?.ActionName ?? "UnknownAction";
        var actor = context.HttpContext.User.Identity?.Name ?? "Anonymous";
        var executedContext = await next();

        if (executedContext.Exception is not null)
            return; // RequestLoggingMiddleware records the exception with its stack trace and trace ID.

        if (!context.ModelState.IsValid)
        {
            _logger.LogWarning(
                "Write operation {Controller}.{Action} by {Actor} was rejected by model validation. Error count: {ErrorCount}",
                controllerName,
                actionName,
                actor,
                context.ModelState.ErrorCount);
            return;
        }

        _logger.LogInformation(
            "Write operation {Controller}.{Action} by {Actor} completed with result {ResultType}. TraceId: {TraceId}",
            controllerName,
            actionName,
            actor,
            executedContext.Result?.GetType().Name ?? "No result",
            context.HttpContext.TraceIdentifier);
    }
}
