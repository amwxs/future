using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Users.Commands.CreateUser;

namespace Zoo.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateUser")]
        
        public async Task<bool> CreateUser([FromBody] CreateUserCommand  command)
        {
           return await _sender.Send(command);
        }
    }
}
