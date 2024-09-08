using Crossroads.Application.Features.AppUser.Commands.AddAppUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crossroads.WebAPI.Controllers.Account
{
    [Area("Account")]
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : Controller
    {
        private readonly IMediator _mediator;
        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Create(AddAppUserCommand addAppUserCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(addAppUserCommand);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
