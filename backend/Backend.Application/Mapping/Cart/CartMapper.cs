namespace Backend.Application.Mapping.Cart;

using System.Linq;
using Backend.Application.DTOs.Cart;
using Domain.AggregatesModel.CartAggregate;

public static class CartMapper
{
    public static CartDto MapToCartDto(Cart cart)
    {
        return new CartDto(
            Id: cart.Id,
            UserId: cart.UserId,
            Items: cart.Items.ToList());
    }
}