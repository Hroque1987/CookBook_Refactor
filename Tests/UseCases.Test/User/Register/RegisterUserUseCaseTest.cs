using System;
using Application.Services;
using Application.UseCases;
using Application.Validators;
using Bogus.DataSets;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Respositories;
using Exceptions;
using Exceptions.Exceptionbase;
using Microsoft.VisualBasic;
using Shouldly;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldSatisfyAllConditions(result => result.Name.ShouldBe(request.Name));

    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);
    
        var exception =  await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.ShouldSatisfyAllConditions(exception => exception.ErrorMessages.ShouldHaveSingleItem(),
                                            exception =>exception.ErrorMessages.ShouldContain( 
                                                error => error == ResourceMessagesException.EMAIL_EXISTS));
        
    }

    [Theory]
    [InlineData("")]
    public async Task Error_Name_Empty(string name)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);
    
        await act.ShouldThrowAsync<ErrorOnValidationException>();
        
    }


    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        
        var validator = new RegisterUserValidator();
        var mapper = MapperBuilder.Build();
        var encrypter = PasswordEncrypterBuilder.Build(); 
        var writeRepository = UserWriteOnlyRespositoryBuilder.Builder();
        var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if(string.IsNullOrEmpty(email) == false)
            readRepositoryBuilder.ExistsActiveUserWithEmail(email);
        
        return new RegisterUserUseCase(validator, encrypter, writeRepository, mapper,unitOfWork, readRepositoryBuilder.Build());
    }
}
