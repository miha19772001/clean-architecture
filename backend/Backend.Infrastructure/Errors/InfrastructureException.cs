namespace Backend.Infrastructure.Errors;

using System.Net;

public class InfrastructureException(
    string message,
    string extensionCode,
    HttpStatusCode statusCode)
    : Exception(message)
{
    public string ExtensionCode { get; } = extensionCode;

    public HttpStatusCode StatusCode { get; } = statusCode;
}