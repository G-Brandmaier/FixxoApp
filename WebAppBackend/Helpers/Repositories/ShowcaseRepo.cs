using Microsoft.EntityFrameworkCore;
using WebAppBackend.Contexts;
using WebAppBackend.Models.Dtos;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Helpers.Repositories;

public class ShowcaseRepo
{
    private readonly IdentityContext _identityContext;

    public ShowcaseRepo(IdentityContext context)
    {
        _identityContext = context;
    }

    public async Task<ShowcaseModel> GetShowcase()
    {
        var resultShowcaseEntity = await _identityContext.Showcases.OrderByDescending(x => x.Id).FirstAsync();
        ShowcaseModel model = resultShowcaseEntity;
        return model;
    }
    public async Task<bool> AddShowcase(ShowcaseModel model)
    {
        await _identityContext.Showcases.AddAsync(model);
        await _identityContext.SaveChangesAsync();
        return true;
    }

}
