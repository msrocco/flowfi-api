using FlowFi.Communication.Requests;
using FluentValidation;

namespace FlowFi.Application.UseCases.Auth.SignUp;

public class SignUpValidator : AbstractValidator<RequestSignUpJson>
{
    public SignUpValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("The name cannot be empty."); // ResourceErrorMessages.NAME_EMPTY
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("The email cannot be empty.") // ResourceErrorMessages.EMAIL_EMPTY
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage("The email is invalid."); // ResourceErrorMessages.EMAIL_INVALID
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestSignUpJson>());
    }
}
