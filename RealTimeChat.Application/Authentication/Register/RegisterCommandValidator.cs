using FluentValidation;

namespace RealTimeChat.Application.Authentication.Register
{
  public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
  {
    public RegisterCommandValidator()
    {
      RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .WithMessage("Email isnt valid.");

      RuleFor(x => x.FullName)
        .NotEmpty()
        .WithMessage("Name is required.")
        .MaximumLength(100)
        .WithMessage("Name is too long.");

      RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .WithMessage("Passowrd must be 8 characters at least.");


      RuleFor(x => x.ConfirmPassword)
        .Equal(x => x.Password)
        .WithMessage("Password don't match.");
    }
  }
}