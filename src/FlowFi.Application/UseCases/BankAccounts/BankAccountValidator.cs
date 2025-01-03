using FlowFi.Communication.Requests;
using FlowFi.Exception;
using FluentValidation;

namespace FlowFi.Application.UseCases.BankAccounts;

public class BankAccountValidator : AbstractValidator<RequestBankAccountJson>
{
    public BankAccountValidator()
    {
        RuleFor(bankAccount => bankAccount.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(bankAccount => bankAccount.InitialBalance).GreaterThanOrEqualTo(0).NotEmpty().WithMessage(ResourceErrorMessages.BALANCE_REQUIRED);
        RuleFor(bankAccount => bankAccount.Type).NotEmpty().WithMessage(ResourceErrorMessages.BANK_ACCOUNT_TYPE_REQUIRED);
        RuleFor(bankAccount => bankAccount.Color).NotEmpty().WithMessage(ResourceErrorMessages.COLOR_REQUIRED);
    }
}
