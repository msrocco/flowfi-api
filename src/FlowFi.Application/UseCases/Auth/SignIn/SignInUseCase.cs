using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;
using FlowFi.Domain.Repositories.User;
using FlowFi.Domain.Security.Cryptography;
using FlowFi.Domain.Security.Tokens;
using FlowFi.Exception.ExceptionsBase;

namespace FlowFi.Application.UseCases.Auth.SignIn;

public class SignInUseCase : ISignInUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public SignInUseCase(
    IUserReadOnlyRepository repository,
    IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseSignUpJson> Execute(RequestSignInJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email) ?? throw new InvalidCredentialsException("Invalid credentials.");

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordMatch == false)
        {
            throw new InvalidCredentialsException("Invalid credentials.");
        }

        return new ResponseSignUpJson
        {
            AccessToken = _accessTokenGenerator.Generate(user)
        };
    }
}
