namespace Backend.Domain.AggregatesModel.CartAggregate;

using Common;
using Errors;

public class Cart : AggregateRoot
{
    private readonly List<CartItem> _cartItems;

    [Obsolete("Only for EF", true)]
    public Cart()
    {
    }

    private Cart(Guid userId)
    {
        UserId = userId;
        _cartItems = [];
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<CartItem> CartItems => _cartItems;

    public static Cart Create(Guid userId)
    {
        if (userId == Guid.Empty)
            DomainErrors.General.ValueIsRequired(nameof(UserId));

        return new Cart(userId);
    }

    public Guid AddCartItem(Guid productId, int quantity)
    {
        var cartItem = CartItem.Create(Id, productId, quantity);

        var existing = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (existing is not null)
        {
            existing.SetQuantity(quantity);
        }
        else
        {
            _cartItems.Add(cartItem);
        }

        return cartItem.Id;
    }

    public void EditQuantityCartItem(Guid productId, int newQuantity)
    {
        if (newQuantity < 0)
            throw DomainErrors.Cart.QuantityNotValid();

        var cartItem = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem is null)
            throw DomainErrors.Cart.ItemNotFound();

        cartItem.SetQuantity(newQuantity);
    }

    public Guid RemoveCartItem(Guid productId)
    {
        var cartItem = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem is null)
            throw DomainErrors.Cart.ItemNotFound();

        _cartItems.Remove(cartItem);

        return cartItem.Id;
    }
}