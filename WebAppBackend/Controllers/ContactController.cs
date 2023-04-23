using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Filters;
using WebAppBackend.Helpers.Repositories;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Controllers
{
    [UseApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly CommentRepo _commentRepo;

        public ContactController(CommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpPost]
        public async Task<IActionResult> SendComment(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _commentRepo.AddComment(model);
                if(result)
                    return Created("", null);
            }
            return BadRequest(model);
        }
    }
}
