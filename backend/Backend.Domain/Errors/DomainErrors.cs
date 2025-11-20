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
        public static BusinessLogicException NotFound() =>
            new($"Cart is not found", nameof(NotFound), HttpStatusCode.NotFound);

        public static BusinessLogicException QuantityNotValid() =>
            new($"Quantity is not valid", nameof(QuantityNotValid), HttpStatusCode.Conflict);

        public static BusinessLogicException ItemNotFound() =>
            new($"Cart item is not found", nameof(ItemNotFound), HttpStatusCode.NotFound);
    }

    public static class Product
    {
        public static BusinessLogicException NotFound() =>
            new($"Product is not found", nameof(NotFound), HttpStatusCode.NotFound);

        public static BusinessLogicException ProductIsOutOfStock() =>
            new($"Product is out of stock", nameof(ProductIsOutOfStock), HttpStatusCode.NotFound);
    }

    public static class User
    {
        public static BusinessLogicException NotFound() =>
            new($"User not found", nameof(NotFound), HttpStatusCode.NotFound);

        public static BusinessLogicException WrongPassword() =>
            new($"Password is wrong", nameof(WrongPassword), HttpStatusCode.NotFound);

        public static BusinessLogicException HasUserWithSameLogin() =>
            new($"User with this login already exists", nameof(HasUserWithSameLogin), HttpStatusCode.Conflict);

        public static BusinessLogicException NoRights() =>
            new($"No rights", nameof(NoRights), HttpStatusCode.Conflict);
    }

    public static class Session
    {
        public static BusinessLogicException NotFound() =>
            new($"Session not found", nameof(NotFound), HttpStatusCode.NotFound);
    }

    public static class File
    {
        public static BusinessLogicException NotFound() =>
            new($"File not found", nameof(NotFound), HttpStatusCode.NotFound);

        public static BusinessLogicException HasDifferentType() =>
            new($"File is of a different type", nameof(HasDifferentType), HttpStatusCode.Conflict);
    }
}