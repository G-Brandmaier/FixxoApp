using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAppBackend.Helpers.Repositories;
using WebAppBackend.Models.Dtos;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Helpers.Services;

public class AuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleService _roleService;
    private readonly UserRepo _userRepo;
    private readonly TokenGenerator _tokenGenerator;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleService roleService, UserRepo userRepo, TokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleService = roleService;
        _userRepo = userRepo;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<bool> RegisterAsync(RegisterUserModel model)
    {
        try
        {
            var role = await _roleService.CheckIfRolesExist();
            if (!string.IsNullOrEmpty(role))
                model.RoleName = role;

            IdentityUser identityUser = model;

            var identityUserResult = await _userManager.CreateAsync(identityUser, model.Password);
            if (identityUserResult.Succeeded)
            {
                UserEntity userEntity = model;
                userEntity.Id = identityUser.Id;
                userEntity.Email = identityUser.Email!;
                if (await _userRepo.AddUserAsync(userEntity))
                {
                    await _userManager.AddToRoleAsync(identityUser, model.RoleName!);
                    return true;
                }
            }
        }
        catch { }
        return false;
    }

    public async Task<string> LogInAsync(LoginModel model)
    {
        var identityUser = await _userManager.FindByEmailAsync(model.Email);
        if (identityUser != null)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(identityUser, model.Password, false);
            if (signInResult.Succeeded)
            {
                var list = await _userManager.GetRolesAsync(identityUser);


                var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", identityUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, identityUser.Email!),
                    new Claim(ClaimTypes.Role, list[0])
                });
                return _tokenGenerator.GenerateJwtToken(claimsIdentity, DateTime.Now.AddHours(1));
            }
        }
        return string.Empty;
    }
    public async Task<UserProfileModel> GetUserAsync(string email)
    {
        UserProfileModel model = await _userRepo.GetUserByIdAsync(email);

        return model;
    }
}