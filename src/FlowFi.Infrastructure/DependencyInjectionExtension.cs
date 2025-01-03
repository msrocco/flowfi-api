using FlowFi.Domain.Repositories;
using FlowFi.Domain.Repositories.BankAccount;
using FlowFi.Domain.Repositories.Category;
using FlowFi.Domain.Repositories.Expenses;
using FlowFi.Domain.Repositories.Transaction;
using FlowFi.Domain.Repositories.User;
using FlowFi.Domain.Security.Cryptography;
using FlowFi.Domain.Security.Tokens;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Infrastructure.DataAccess;
using FlowFi.Infrastructure.DataAccess.Repositories;
using FlowFi.Infrastructure.Extensions;
using FlowFi.Infrastructure.Security.Tokens;
using FlowFi.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlowFi.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddToken(services, configuration);
        AddRepositories(services);

        if (configuration.IsTestEnvironment() == false)
        {
            AddDbContext(services, configuration);
        }

    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    } 
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IBankAccountWriteOnlyRepository, BankAccountRepository>();
        services.AddScoped<IBankAccountReadOnlyRepository, BankAccountRepository>();
        services.AddScoped<ITransactionWriteOnlyRepository, TransactionRepository>();
        services.AddScoped<ICategoryWriteOnlyRepository, CategoryRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<FlowFiDbContext>(config => config.UseNpgsql(connectionString));
    }
}
