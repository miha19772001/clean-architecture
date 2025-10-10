namespace Backend.Domain.Errors;

using System.Net;

public static class DomainErrors
{
    public static class General
    {
        public static BusinessLogicException ValueIsInvalid(string propertyName) =>
            new($"{propertyName} is not valid", nameof(ValueIsInvalid), HttpStatusCode.Conflict);

        public static BusinessLogicException ValueIsRequired(string propertyName) =>
            new($"{propertyName} is required", nameof(ValueIsRequired), HttpStatusCode.Conflict);

        public static BusinessLogicException ValueNotFound(string propertyName) =>
            new($"{propertyName} is not found", nameof(ValueNotFound), HttpStatusCode.NotFound);
    }

    public static class Cart
    {
        public static BusinessLogicException QuantityNotValid() =>
            new($"Quantity is not valid", nameof(QuantityNotValid), HttpStatusCode.Conflict);

        public static BusinessLogicException ItemNotFound() =>
            new($"Cart item is not found", nameof(ItemNotFound), HttpStatusCode.NotFound);
    }
}