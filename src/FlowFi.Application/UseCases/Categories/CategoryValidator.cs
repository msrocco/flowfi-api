using FlowFi.Communication.Requests;
using FlowFi.Exception;
using FluentValidation;

namespace FlowFi.Application.UseCases.Categories;

public class CategoryValidator : AbstractValidator<RequestCategoryJson>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(category => category.Icon).NotEmpty().WithMessage(ResourceErrorMessages.ICON_REQUIRED);
        RuleFor(category => category.Type).NotEmpty().WithMessage(ResourceErrorMessages.CATEGORY_TYPE_REQUIRED);
    }
}
