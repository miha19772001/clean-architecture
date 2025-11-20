namespace Backend.Infrastructure.Errors;

using System.Net;

public static class InfrastructureErrors
{
    public static class General
    {
        public static InfrastructureException ErrorMessage(string message) =>
            new(message, nameof(ErrorMessage), HttpStatusCode.Conflict);
    }
}