namespace Backend.Application.APIHandlers.Cart.GetCartItems;

using FluentValidation;

public class GetCartItemsRequestValidator : AbstractValidator<GetCartItemsRequest>
{
    public GetCartItemsRequestValidator()
    {
    }
}