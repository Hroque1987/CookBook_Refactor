
using Application.UseCases;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Communication.Requests;

namespace Application;

public static class DependencyInjectionExtension
{
    public static void  AddApplication(this IServiceCollection services )
    {
        AddUseCases(services);
        AddValidator(services);
    }


    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    public static void AddValidator(IServiceCollection services)
    {
        services.AddScoped<IValidator<RequestRegisterUserJson>, RegisterUserValidator>();
    }

}
