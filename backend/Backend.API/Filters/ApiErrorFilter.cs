namespace Backend.API.Filters;

using Application.Validation;
using FluentValidation;
using System.Text.Json;
using Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

public class ApiErrorFilter : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var error = context.Exception switch
        {
            BusinessLogicException businessLogicException => DomainErrorFactory.Create(businessLogicException),
            ValidationException validationException => ValidationErrorFactory.Create(validationException),
            _ => ApiError.DefaultApiError
        };

        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)error.StatusCode;

        await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(error));

        context.ExceptionHandled = true;
    }
}