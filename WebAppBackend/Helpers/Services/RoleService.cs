using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAppBackend.Helpers.Services;

public class RoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<string> CheckIfRolesExist()
    {
        if (!await _roleManager.Roles.AnyAsync())
        {
            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("product manager"));
        }
        return await CheckIfAdminExists();
    }
    private async Task<string> CheckIfAdminExists()
    {
        if (!await _userManager.Users.AnyAsync())
            return "admin";
        else
            return null!;
    }
}
