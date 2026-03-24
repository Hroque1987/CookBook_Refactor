using System;
using Application.Validators;
using Communication.Requests;
using Communication.Responses;
using Exceptions.Exceptionbase;
using FluentValidation;

namespace Application.UseCases;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IValidator<RequestRegisterUserJson> _registerUserValidator;
    public RegisterUserUseCase(IValidator<RequestRegisterUserJson> registerUserValidator)
    {
        _registerUserValidator = registerUserValidator;
    }

    public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
    {
  
        Validate(request);
 
        return new ResponseRegisterUserJson()
        {
            Name = request.Name
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var result = _registerUserValidator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }

}
