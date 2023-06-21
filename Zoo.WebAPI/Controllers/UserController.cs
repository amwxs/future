using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Users.Commands.CreateUser;
using Zoo.Application.Users.Queries.GetByUserId;
using Zoo.Domain.Core.Result;

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

        [HttpPost("Create")]
        
        public async Task<CustResult<bool>> CreateUser([FromBody] CreateUserCommand  command)
        {
           return await _sender.Send(command);
        }

        [HttpGet("id")]
        public async Task<CustResult<GetByUserIdRes>>GetByUserId([FromQuery] GetByUserIdQuery query)
        {
            return await _sender.Send(query);
        }
    }

}
