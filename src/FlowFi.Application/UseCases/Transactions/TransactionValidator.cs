using FlowFi.Communication.Requests;
using FlowFi.Exception;
using FluentValidation;

namespace FlowFi.Application.UseCases.Transactions;

public class TransactionValidator : AbstractValidator<RequestTransactionJson>
{
    public TransactionValidator()
    {
        RuleFor(bankAccount => bankAccount.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(bankAccount => bankAccount.Value).GreaterThanOrEqualTo(0).NotEmpty().WithMessage(ResourceErrorMessages.TRANSACTION_VALUE_REQUIRED);
        RuleFor(bankAccount => bankAccount.Type).NotEmpty().WithMessage(ResourceErrorMessages.TRANSACTION_TYPE_REQUIRED);
        RuleFor(bankAccount => bankAccount.CategoryId).NotEmpty().WithMessage(ResourceErrorMessages.CATEGORY_ID_REQUIRED);
        RuleFor(bankAccount => bankAccount.BankAccountId).NotEmpty().WithMessage(ResourceErrorMessages.BANK_ACCOUNT_ID_REQUIRED);
    }
}
