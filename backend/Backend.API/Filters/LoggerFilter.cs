namespace Backend.API.Filters;

using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading.Tasks;

public class LoggerFilter(ILogger<LoggerFilter> logger) : IAsyncActionFilter
{
    private readonly ILogger<LoggerFilter> _logger = logger
                                                     ?? throw new ArgumentNullException(nameof(logger));

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionName = context.ActionDescriptor.DisplayName ?? context.ActionDescriptor.Id;
        var controllerName = context.Controller?.GetType().Name ?? "UnknownController";
        var httpMethod = context.HttpContext.Request.Method;
        var url = context.HttpContext.Request.Path;

        _logger.LogInformation($"START: {httpMethod} {url} -> Action: {actionName}, Controller: {controllerName}");

        var stopwatch = Stopwatch.StartNew();

        var resultContext = await next();

        stopwatch.Stop();

        long duration = stopwatch.ElapsedMilliseconds;

        if (resultContext.Result is ObjectResult objectResult)
        {
            string? resultType = objectResult.GetType().Name;
            int? statusCode = objectResult.StatusCode;

            _logger.LogInformation(
                $"SUCCESS: {controllerName}.{actionName} completed in {duration}ms | " +
                $"ResultType: {resultType} | " +
                $"StatusCode: {statusCode}");
        }
        else if (resultContext.Exception is not null)
        {
            var apiError = DomainErrorFactory.Create(resultContext.Exception);

            _logger.LogError(
                $"FAILED: {controllerName}.{actionName} completed in {duration}ms | " +
                $"Message: {apiError.Message} | " +
                $"StatusCode: {apiError.StatusCode} | " +
                $"ExtensionCode: {apiError.ExtensionCode}");
        }
        else
        {
            _logger.LogWarning(
                $"UNKNOWN: {controllerName}.{actionName} completed in {duration}ms | " +
                $"No result or exception"
            );
        }
    }
}