namespace Backend.Domain.Errors;

using System;
using System.Net;

public static class DomainErrorFactory
{
    public static ApiError Create(Exception? exception)
    {
        if (exception is null)
            return ApiError.DefaultApiError;

        ApiError apiError;

        if (exception is BusinessLogicException businessLogicException)
        {
            apiError = new ApiError(
                businessLogicException.Message,
                businessLogicException.ExtensionCode,
                businessLogicException.StatusCode
            );
        }
        else
        {
            apiError = new ApiError(exception.Message, "Unknown error", HttpStatusCode.InternalServerError);
        }

        return apiError;
    }
}