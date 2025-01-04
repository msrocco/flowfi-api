
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Domain.Repositories.Category;
using FlowFi.Exception.ExceptionsBase;
using FlowFi.Exception;

namespace FlowFi.Application.UseCases.Categories.Delete;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _writeOnlyRepository;
    private readonly ICategoryReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteCategoryUseCase(
        ICategoryWriteOnlyRepository writeOnlyRepository,
        ICategoryReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id)
    {
        var loggedUser = await _loggedUser.Get();

        _ = await _readOnlyRepository.GetById(loggedUser, id)
            ?? throw new NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);

        await _writeOnlyRepository.Delete(id);

        await _unitOfWork.Commit();
    }
}
