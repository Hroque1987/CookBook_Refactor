using System;

namespace Communication.Responses;

public class ResponseErrorJson
{
    public IList<string> Errors {get; set;} 


    public ResponseErrorJson(IList<string> errors) => Errors = errors;
    
    public ResponseErrorJson(string error)
    {
         IList<string> errors = [
             error
         ];
         Errors = errors;
    }
}
