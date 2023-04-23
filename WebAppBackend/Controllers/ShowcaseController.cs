using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Filters;
using WebAppBackend.Helpers.Repositories;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Controllers;

[UseApiKey]
[Route("api/[controller]")]
[ApiController]
public class ShowcaseController : ControllerBase
{
    private readonly ShowcaseRepo _showcaseRepo;

    public ShowcaseController(ShowcaseRepo showcaseRepo)
    {
        _showcaseRepo = showcaseRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetShowcase()
    {
        return Ok(await _showcaseRepo.GetShowcase());
    }

    [HttpPost]
    [Route("AddShowcase")]
    public async Task<IActionResult> AddShowcase(ShowcaseModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _showcaseRepo.AddShowcase(model))
                return Created("", null);
        }
        return BadRequest(model);
    }
}
