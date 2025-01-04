using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Categories.GetAll;

public interface IGetAllCategoriesUseCase
{
    Task<ResponseCategoriesJson> Execute();
}
