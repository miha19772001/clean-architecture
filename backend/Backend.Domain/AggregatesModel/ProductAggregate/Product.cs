namespace Backend.Domain.AggregatesModel.ProductAggregate;

using Common;
using Errors;

public class Product : AggregateRoot
{
    [Obsolete("Only for EF", true)]
    private Product()
    {
    }

    private Product(
        string name,
        string description,
        decimal price,
        int quantity,
        Guid imageId)
    {
        Name = name;
        IsDeleted = false;
        Description = description;
        Price = price;
        Quantity = quantity;
        CreatedAt = DateTime.Now;
        ImageId = imageId;
    }

    public string Name { get; private set; }

    public bool IsDeleted { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Guid ImageId { get; private set; }

    public static Product Create(
        string name,
        string description,
        decimal price,
        int quantity,
        Guid imageId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw DomainErrors.General.ValueIsRequired(nameof(Name));

        if (string.IsNullOrWhiteSpace(description))
            throw DomainErrors.General.ValueIsRequired(nameof(Description));

        if (price <= 0)
            throw DomainErrors.General.ValueIsInvalid(nameof(Price));

        if (quantity < 0)
            throw DomainErrors.General.ValueIsInvalid(nameof(Quantity));

        if (imageId == Guid.Empty)
            throw DomainErrors.General.ValueIsRequired(nameof(ImageId));

        return new Product(name, description, price, quantity, imageId);
    }

    public void AddQuantity() => Quantity += 1;

    public void RemoveQuantity() => Quantity -= 1;
}