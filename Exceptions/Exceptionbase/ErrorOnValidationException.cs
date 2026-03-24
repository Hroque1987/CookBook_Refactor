using System;

namespace Exceptions.Exceptionbase;

public class ErrorOnValidationException(IList<string> errors) : MyRecipyBookException
{
   public IList<string> ErrorMessages {get; set;} = errors;
}
