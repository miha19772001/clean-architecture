namespace Backend.Application.APIHandlers.Account.Register;

using FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}