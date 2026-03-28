using System;
using Application.Validators;
using CommonTestUtilities.Requests;
using Exceptions;
using Shouldly;



namespace Validation.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(RequestRegisterUserJsonBuilder.Build());

        result.IsValid.ShouldSatisfyAllConditions(result => result.ShouldBeTrue());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Error_Name_Empty(string name)
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);
        
        result.Errors.ShouldSatisfyAllConditions(result => result.ShouldHaveSingleItem(), 
                                                result => result.ShouldContain(
                                                error => error.ErrorMessage == ResourceMessagesException.NAME_EMPTY ) );
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);
        
        result.Errors.ShouldSatisfyAllConditions(result => result.ShouldHaveSingleItem(), 
                                                result => result.ShouldContain(
                                                error => error.ErrorMessage == ResourceMessagesException.EMPTY_EMAIL ) );
    }

    [Theory]
    [InlineData("email.com")]
    [InlineData("@.com")]
    public void Error_Email_Invalid(string email)
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);
        
        result.Errors.ShouldSatisfyAllConditions(result => result.ShouldHaveSingleItem(), 
                                                result => result.ShouldContain(
                                                error => error.ErrorMessage == ResourceMessagesException.INVALID_EMAIL ) );
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

        var result = validator.Validate(request);
        
        result.Errors.ShouldSatisfyAllConditions(result => result.ShouldHaveSingleItem(), 
                                                result => result.ShouldContain(
                                                error => error.ErrorMessage == ResourceMessagesException.INVALID_PASSWORD ) );
    }

}
