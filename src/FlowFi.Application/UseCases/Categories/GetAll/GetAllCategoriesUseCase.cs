using AutoMapper;
using FlowFi.Communication.Responses;
using FlowFi.Domain.Repositories.Category;
using FlowFi.Domain.Services.LoggedUser;

namespace FlowFi.Application.UseCases.Categories.GetAll;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
{
    private readonly ICategoryReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllCategoriesUseCase(ICategoryReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseCategoriesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser);

        return new ResponseCategoriesJson
        {
            Categories = _mapper.Map<List<ResponseShortCategoryJson>>(result)
        };
    }
}
