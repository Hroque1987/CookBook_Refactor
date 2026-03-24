using System;
using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases;

public interface IRegisterUserUseCase
{
 public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
