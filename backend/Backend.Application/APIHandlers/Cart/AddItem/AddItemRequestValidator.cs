namespace Backend.Application.APIHandlers.Cart.AddItem;

using FluentValidation;

public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
{
    public AddItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product Id is required");
    }
}