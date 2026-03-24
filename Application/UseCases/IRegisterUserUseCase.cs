using System;
using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases;

public interface IRegisterUserUseCase
{
 public ResponseRegisterUserJson Execute(RequestRegisterUserJson request);
}
