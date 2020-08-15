namespace CarRentalSystem.Application.Features.Identity.Commands.RegisterUser
{
    using FluentValidation;
    using static Domain.Models.ModelConstants;

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            this.RuleFor(u => u.Email)
                .MinimumLength(Common.MIN_EMAIL_LENGTH)
                .MaximumLength(Common.MAX_EMAIL_LENGTH)
                .EmailAddress()
                .NotEmpty();

            this.RuleFor(u => u.Password)
                .MaximumLength(Common.MAX_NAME_LENGTH)
                .NotEmpty();

            this.RuleFor(u => u.Name)
                .MinimumLength(Common.MIN_NAME_LENGTH)
                .MaximumLength(Common.MAX_NAME_LENGTH)
                .NotEmpty();
        }
    }
}
