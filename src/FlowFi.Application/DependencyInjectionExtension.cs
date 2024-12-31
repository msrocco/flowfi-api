using FlowFi.Application.AutoMapper;
using FlowFi.Application.UseCases.Auth.SignIn;
using FlowFi.Application.UseCases.BankAccounts.Create;
using FlowFi.Application.UseCases.Users.Profile;
using FlowFi.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace FlowFi.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<ICreateBankAccountUseCase, CreateBankAccountUseCase>();
    } 
}
