namespace Backend.API.Filters;

using FluentValidation;
using Application.Validation;
using Infrastructure.Errors;
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
        var controllerName = context.ActionDescriptor.RouteValues["controller"] ?? "UnknownController";
        var actionName = context.ActionDescriptor.RouteValues["action"] ?? "UnknownAction";
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
            var apiError = resultContext.Exception switch
            {
                BusinessLogicException businessLogicException => DomainErrorFactory.Create(businessLogicException),
                InfrastructureException infrastructureException => InfrastructureErrorFactory.Create(
                    infrastructureException),
                ValidationException validationException => ValidationErrorFactory.Create(validationException),
                _ => ApiError.DefaultApiError
            };

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