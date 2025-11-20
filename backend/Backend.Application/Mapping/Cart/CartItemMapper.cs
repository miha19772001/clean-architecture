namespace Backend.Application.Mapping.Cart;

using Backend.Application.DTOs.Cart;
using Domain.AggregatesModel.CartAggregate;

public static class CartItemMapper
{
    public static CartItemDto MapToCartItemDto(CartItem cartItem)
    {
        return new CartItemDto(
            Id: cartItem.Id,
            ProductId: cartItem.ProductId,
            Quantity: cartItem.Quantity);
    }
}