namespace Backend.Application.Validation;

using System;
using System.Net;
using Domain.Errors;
using System.Linq;
using FluentValidation;

public static class ValidationErrorFactory
{
    public static ApiError Create(Exception? exception)
    {
        if (exception is null)
            return ApiError.DefaultApiError;

        ApiError apiError;

        if (exception is ValidationException validationException)
        {
            var error = validationException.Errors.ToArray()[0];

            apiError = new ApiError(
                error.ErrorMessage,
                error.PropertyName,
                HttpStatusCode.Conflict
            );
        }
        else
        {
            apiError = new ApiError(exception.Message, "Unknown error", HttpStatusCode.InternalServerError);
        }

        return apiError;
    }
}