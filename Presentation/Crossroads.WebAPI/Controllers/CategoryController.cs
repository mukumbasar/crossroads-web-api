using Crossroads.Application.Features.Category.Commands.AddCategory;
using Crossroads.Application.Features.Category.Queries.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "AppUser, Admin")]
        public async Task<IActionResult> Create(AddCategoryCommand addCategoryCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(addCategoryCommand);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("categories")]
        [Authorize(Roles ="AppUser, Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
