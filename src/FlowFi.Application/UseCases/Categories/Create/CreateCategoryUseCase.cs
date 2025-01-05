using AutoMapper;
using FlowFi.Communication.Requests;
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Domain.Repositories.Category;
using FlowFi.Exception.ExceptionsBase;
using FlowFi.Domain.Entities;

namespace FlowFi.Application.UseCases.Categories.Create;

public class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public CreateCategoryUseCase(ICategoryWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestCategoryJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var category = _mapper.Map<Category>(request);
        category.UserId = loggedUser.Id;

        await _repository.Add(category);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestCategoryJson request)
    {
        var validator = new CategoryValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
