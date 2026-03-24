
using Application.UseCases;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Communication.Requests;
using Microsoft.Extensions.Options;
using Application.Services;
using AutoMapper;
using Application.Services.AutoMapper;
using Microsoft.Extensions.Configuration;


namespace Application;

public static class DependencyInjectionExtension
{
    public static void  AddApplication(this IServiceCollection services, IConfiguration configuration )
    {
        AddUseCases(services);
        AddValidator(services);
        AddPasswordEncrypter(services, configuration);
        AddAutoMapper(services);
    }


    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    public static void AddValidator(IServiceCollection services)
    {
        services.AddScoped<IValidator<RequestRegisterUserJson>, RegisterUserValidator>();
    }

    public static void AddPasswordEncrypter(IServiceCollection services,  IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped(options => new PasswordEncrypter(additionalKey!));
    }

    public static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(options => new AutoMapper.MapperConfiguration(options => options.AddProfile(new AutoMapping())).CreateMapper());
    }

}
