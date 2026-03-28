using System;
using Application.Services;

namespace CommonTestUtilities.Criptography;

public class PasswordEncrypterBuilder
{
    public static PasswordEncrypter Build() => new("abs1234"); // Same as New PasswordEncrypter("abs1234")
    
}
