using Application.UseCases;
using Communication.Requests;
using Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created) ]
        public IActionResult Register([FromServices]IRegisterUserUseCase usecase, [FromBody]RequestRegisterUserJson request)
        { 
           var result = usecase.Execute(request);
            return Created(string.Empty, result);
             
        } 
    }
}
