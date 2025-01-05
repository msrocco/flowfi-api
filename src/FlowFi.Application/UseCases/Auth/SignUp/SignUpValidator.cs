using FlowFi.Communication.Requests;
using FlowFi.Exception;
using FluentValidation;

namespace FlowFi.Application.UseCases.Auth.SignUp;

public class SignUpValidator : AbstractValidator<RequestSignUpJson>
{
    public SignUpValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestSignUpJson>());
    }
}
