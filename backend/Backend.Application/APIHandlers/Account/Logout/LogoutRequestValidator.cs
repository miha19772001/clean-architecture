namespace Backend.Application.APIHandlers.Account.Logout;

using FluentValidation;

public class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
    }
}