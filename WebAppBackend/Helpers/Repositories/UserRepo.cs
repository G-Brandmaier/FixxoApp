using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Contexts;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Helpers.Repositories;

public class UserRepo
{
    private readonly IdentityContext _identityContext;

    public UserRepo(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<bool> AddUserAsync(UserEntity entity)
    {
        try
        {
            await _identityContext.UserProfiles.AddAsync(entity);
            await _identityContext.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }

    public async Task<UserEntity> GetUserByIdAsync(string email)
    {
        var userResponse = await _identityContext.UserProfiles.Where(u => u.Email == email).FirstOrDefaultAsync();
        return userResponse!;
    }

}
