using Communication.Requests;
using FluentValidation;
using Exceptions;

namespace Application.Validators;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{

    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);//ResourceMessagesException.NAME_EMPTY
        RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_EMAIL);//ResourceMessagesException.INVALID_EMAIL
        RuleFor(user => user.Password).MinimumLength(6).WithMessage(ResourceMessagesException.INVALID_PASSWORD);//ResourceMessagesException.INVALID_PASSWORD
    }

}
