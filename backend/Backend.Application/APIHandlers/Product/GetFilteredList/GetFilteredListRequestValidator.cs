namespace Backend.Application.APIHandlers.Product.GetFilteredList;

using FluentValidation;

public class GetFilteredListRequestValidator : AbstractValidator<GetFilteredListRequest>
{
    public GetFilteredListRequestValidator()
    {
        RuleFor(x => x.Pagination.Count)
            .GreaterThan(0).WithMessage("The count must be greater than 0");

        RuleFor(x => x.Pagination.Offset)
            .GreaterThan(-1).WithMessage("The offset must have a positive value");
    }
}