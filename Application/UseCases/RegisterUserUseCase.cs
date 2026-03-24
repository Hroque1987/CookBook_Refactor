using System;
using Application.Services;
using Application.Services.AutoMapper;
using Application.Validators;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Domain.Repositories.User;
using Exceptions;
using Exceptions.Exceptionbase;
using FluentValidation;
using FluentValidation.Results;

namespace Application.UseCases;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IValidator<RequestRegisterUserJson> _registerUserValidator;
    private readonly PasswordEncrypter _passwordEncrypter;
    private readonly IUserWriteOnlyRespository _userWriteOnlyRespository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    public RegisterUserUseCase(IValidator<RequestRegisterUserJson> registerUserValidator, 
                                        PasswordEncrypter passwordEncrypter, IUserWriteOnlyRespository userWriteOnlyRespository, 
                                        IMapper mapper, IUnitOfWork unitOfWork, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _registerUserValidator = registerUserValidator;
        _passwordEncrypter = passwordEncrypter;
        _userWriteOnlyRespository = userWriteOnlyRespository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
  
        await Validate(request);

        var user= _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _userWriteOnlyRespository.Add(user);
        
        await _unitOfWork.Commit();
 
        return new ResponseRegisterUserJson()
        {
            Name = request.Name
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = _registerUserValidator.Validate(request);

        var emailExists = await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_EXISTS ));
        }

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
        
    }

}
