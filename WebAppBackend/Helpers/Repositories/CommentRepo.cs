using WebAppBackend.Contexts;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Helpers.Repositories;

public class CommentRepo
{
    private readonly IdentityContext _identityContext;

    public CommentRepo(IdentityContext context)
    {
        _identityContext = context;
    }

    public async Task<bool> AddComment(CommentModel model)
    {
        await _identityContext.Comments.AddAsync(model);
        var result = await _identityContext.SaveChangesAsync();
        return result > 0 ? true : false;
    }
}
