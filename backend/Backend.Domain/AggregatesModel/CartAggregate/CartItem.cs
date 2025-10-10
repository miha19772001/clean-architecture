namespace Backend.Domain.AggregatesModel.CartAggregate;

using Common;
using Errors;

public class CartItem : Entity
{
    [Obsolete("Only for EF", true)]
    public CartItem()
    {
    }

    private CartItem(
        Guid cartId,
        Guid productId,
        int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid CartId { get; private set; }
    public Cart? Cart { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public static CartItem Create(Guid cartId, Guid productId, int quantity)
    {
        if (cartId == Guid.Empty)
            throw DomainErrors.General.ValueIsRequired(nameof(CartId));

        if (productId == Guid.Empty)
            throw DomainErrors.General.ValueIsRequired(nameof(ProductId));

        if (quantity < 0)
            throw DomainErrors.General.ValueIsInvalid(nameof(Quantity));

        return new CartItem(cartId, productId, quantity);
    }

    public void SetQuantity(int quantity) => Quantity = quantity;
}