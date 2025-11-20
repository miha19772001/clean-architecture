namespace Backend.Application.APIHandlers.Cart.RemoveItem;

using FluentValidation;

public class RemoveItemRequestValidator : AbstractValidator<RemoveItemRequest>
{
    public RemoveItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product Id is required");
    }
}