using Communication.Requests;
using FluentValidation;
using Exceptions;

namespace Application.Validators;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{

    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_EMAIL);
        RuleFor(user => user.Password).MinimumLength(6).WithMessage(ResourceMessagesException.INVALID_PASSWORD);
        When(user => string.IsNullOrEmpty(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_EMAIL);
        });
    }

}
