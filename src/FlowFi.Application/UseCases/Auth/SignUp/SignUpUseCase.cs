using AutoMapper;
using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;
using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Repositories.Category;
using FlowFi.Domain.Repositories.User;
using FlowFi.Domain.Security.Cryptography;
using FlowFi.Domain.Security.Tokens;
using FlowFi.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace FlowFi.Application.UseCases.Auth.SignUp;

public class SignUpUseCase : ISignUpUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public SignUpUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        ICategoryWriteOnlyRepository categoryWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseSignUpJson> Execute(RequestSignUpJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);

        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.Id = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        var defaultCategories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Salary", Icon = "salary", Type = "INCOME", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Freelance", Icon = "freelance", Type = "INCOME", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Other", Icon = "other", Type = "INCOME", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Home", Icon = "home", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Food", Icon = "food", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Education", Icon = "education", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Leisure", Icon = "fun", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Groceries", Icon = "grocery", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Clothes", Icon = "clothes", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Transport", Icon = "transport", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Travel", Icon = "travel", Type = "EXPENSE", UserId = user.Id },
            new Category { Id = Guid.NewGuid(), Name = "Other", Icon = "other", Type = "EXPENSE", UserId = user.Id }
        };

        await _categoryWriteOnlyRepository.AddRange(defaultCategories);

        await _unitOfWork.Commit();

        return new ResponseSignUpJson
        {
            AccessToken = _tokenGenerator.Generate(user),
        };
    }

    private async Task Validate(RequestSignUpJson request)
    {
        var result = new SignUpValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "This email is already in use."));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
