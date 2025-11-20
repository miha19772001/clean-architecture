namespace Backend.Domain.AggregatesModel.CartAggregate;

using Common;
using Errors;

public class Cart : AggregateRoot
{
    private readonly List<CartItem> _items = [];

    [Obsolete("Only for EF", true)]
    private Cart()
    {
    }

    private Cart(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items;

    public static Cart Create(Guid userId)
    {
        if (userId == Guid.Empty)
            DomainErrors.General.ValueIsRequired(nameof(UserId));

        return new Cart(userId);
    }

    public CartItem AddItem(Guid productId)
    {
        var cartItem = _items.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem is null)
        {
            cartItem = CartItem.Create(Id, productId, 1);
            _items.Add(cartItem);
        }
        else
        {
            cartItem.AddQuantity();
        }

        return cartItem;
    }

    public CartItem RemoveItem(Guid productId)
    {
        var cartItem = _items.FirstOrDefault(ci => ci.ProductId == productId);

        if (cartItem is null)
            throw DomainErrors.Cart.ItemNotFound();

        if (cartItem.Quantity > 1)
        {
            cartItem.RemoveQuantity();
        }
        else
        {
            _items.Remove(cartItem);
        }

        return cartItem;
    }
}