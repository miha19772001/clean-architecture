namespace Backend.Infrastructure.Errors;

using Backend.Domain.Errors;
using System.Net;

public static class InfrastructureErrorFactory
{
    public static ApiError Create(Exception? exception)
    {
        if (exception is null)
            return ApiError.DefaultApiError;

        ApiError apiError;

        if (exception is InfrastructureException infrastructureException)
        {
            apiError = new ApiError(
                infrastructureException.Message,
                infrastructureException.ExtensionCode,
                infrastructureException.StatusCode
            );
        }
        else
        {
            apiError = new ApiError(exception.Message, "Unknown error", HttpStatusCode.InternalServerError);
        }

        return apiError;
    }
}