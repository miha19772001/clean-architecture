namespace Backend.Domain.Errors;

using System.Net;
using System;

public class BusinessLogicException(string message, string extensionCode, HttpStatusCode statusCode)
    : Exception(message)
{
    public string ExtensionCode { get; } = extensionCode;

    public HttpStatusCode StatusCode { get; } = statusCode;
}