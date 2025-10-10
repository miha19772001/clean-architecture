namespace Backend.Domain.Errors;

using System.Net;

public record ApiError(string Message, string ExtensionCode, HttpStatusCode StatusCode)
{
    public string Message { get; } = Message;

    public string ExtensionCode { get; } = ExtensionCode;

    public HttpStatusCode StatusCode { get; } = StatusCode;

    public static ApiError DefaultApiError { get; } =
        new ApiError("Неизвестная ошибка", "Unknown error", HttpStatusCode.InternalServerError);
}