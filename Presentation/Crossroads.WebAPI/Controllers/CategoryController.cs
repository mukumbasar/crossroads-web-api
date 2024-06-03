using Crossroads.Application.Features.Category.Commands.AddCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crossroads.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AddCategoryCommand addCategoryCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(addCategoryCommand);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
