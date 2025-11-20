namespace Backend.Application.APIHandlers.Account.GetCurrent;

using FluentValidation;

public class GetCurrentRequestValidator : AbstractValidator<GetCurrentRequest>
{
    public GetCurrentRequestValidator()
    {
    }
}