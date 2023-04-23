using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Filters;
using WebAppBackend.Helpers.Services;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Controllers;


[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AuthService _authService;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    #region Register
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterUserModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (await _authService.RegisterAsync(model))
                    return Created("", null);
            }
            return BadRequest(model);
        }
        catch { return Problem(); }
    }

    #endregion


    #region Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var token = await _authService.LogInAsync(model);

            if (!string.IsNullOrEmpty(token))
                return Ok(token);
        }
        return Unauthorized("Incorrect email or password");
    }

    #endregion

    #region GetUserInfo

    [Authorize]
    [HttpGet]
    [Route("MyAccount")]
    public async Task<IActionResult> GetMyAccount()
    {
        var user = await _authService.GetUserAsync(User.Identity!.Name!);
        if (user != null) 
            return Ok(user);

        return BadRequest();
    }

    #endregion

    [HttpGet]
    [Route("CheckLogedIn")]
    public async Task<IActionResult> CheckLogedIn()
    {
        HttpContext.User = User;

        if (!string.IsNullOrEmpty(User.Identities.Select(x => x.Name).FirstOrDefault()))
            return Ok(true);

        return Unauthorized(false);
    }

}
