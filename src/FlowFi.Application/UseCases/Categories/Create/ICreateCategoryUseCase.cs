using FlowFi.Communication.Requests;

namespace FlowFi.Application.UseCases.Categories.Create;

public interface ICreateCategoryUseCase
{
    Task Execute(RequestCategoryJson request);
}
