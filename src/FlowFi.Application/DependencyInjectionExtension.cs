﻿using FlowFi.Application.AutoMapper;
using FlowFi.Application.UseCases.Auth.SignIn;
using FlowFi.Application.UseCases.Auth.SignUp;
using FlowFi.Application.UseCases.BankAccounts.Create;
using FlowFi.Application.UseCases.BankAccounts.Delete;
using FlowFi.Application.UseCases.BankAccounts.GetAll;
using FlowFi.Application.UseCases.BankAccounts.GetById;
using FlowFi.Application.UseCases.BankAccounts.Update;
using FlowFi.Application.UseCases.Categories.Create;
using FlowFi.Application.UseCases.Categories.Delete;
using FlowFi.Application.UseCases.Categories.GetAll;
using FlowFi.Application.UseCases.Transactions.Create;
using FlowFi.Application.UseCases.Transactions.Delete;
using FlowFi.Application.UseCases.Transactions.GetAll;
using FlowFi.Application.UseCases.Transactions.Update;
using FlowFi.Application.UseCases.Users.Profile;
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
        services.AddScoped<IGetAllBankAccountsUseCase, GetAllBankAccountsUseCase>();
        services.AddScoped<IGetBankAccountByIdUseCase, GetBankAccountByIdUseCase>();
        services.AddScoped<IDeleteBankAccountUseCase, DeleteBankAccountUseCase>();
        services.AddScoped<IUpdateBankAccountUseCase, UpdateBankAccountUseCase>();
        services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        services.AddScoped<IGetAllTransactionsUseCase, GetAllTransactionsUseCase>();
        services.AddScoped<IUpdateTransactionUseCase, UpdateTransactionUseCase>();
        services.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();
        services.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
    } 
}
