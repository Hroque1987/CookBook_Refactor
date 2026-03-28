using System;
using Bogus;
using Communication.Requests;

namespace CommonTestUtilities.Requests;
//https://github.com/bchavez/Bogus
public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLength = 10)
    {
        return new Faker<RequestRegisterUserJson>()
        .RuleFor(user => user.Name, (fake) => fake.Person.FirstName)
        .RuleFor(user => user.Email, (fake, user) => fake.Internet.Email(user.Name))
        .RuleFor(user => user.Password, (fake) => fake.Internet.Password(passwordLength));

    }

}
