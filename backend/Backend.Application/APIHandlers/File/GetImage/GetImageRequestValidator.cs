namespace Backend.Application.APIHandlers.File.GetImage;

using FluentValidation;

public class GetImageRequestValidator : AbstractValidator<GetImageRequest>
{
    public GetImageRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}