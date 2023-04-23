using Microsoft.EntityFrameworkCore;
using WebAppBackend.Contexts;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Helpers.Repositories;

public class ProductRepo
{
    private readonly IdentityContext _identityContext;

    public ProductRepo(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }
    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        try
        {
            var products = await _identityContext.Products.Include("Tag").ToListAsync();
            return products;
        }
        catch { return null!; }
    }
    public async Task<bool> CreateProductAsync(ProductEntity newProduct)
    {
        await _identityContext.Products.AddAsync(newProduct);
        await _identityContext.SaveChangesAsync();
        return true;
    }

    public async Task<ProductEntity> GetProductByIdAsync(string id)
    {
        var result = await _identityContext.Products.Include("Tag").Where(p => p.ArticleNumber == id).FirstOrDefaultAsync();
        if (result != null)
            return result;

        return null!;
    }

    public async Task<TagEntity> GetTagbyNameAsync(string name)
    {
        var result = await _identityContext.Tags.Where(x => x.Name == name).FirstOrDefaultAsync();
        return result!;

    }

    public async Task<bool> AddTagAsync(TagEntity newTag)
    {        
        try
        {
            await _identityContext.Tags.AddAsync(newTag);
            await _identityContext.SaveChangesAsync();
            return true;
        }
        catch { }
        return false;
    }
    public async Task<IEnumerable<ProductEntity>> GetProductsByTagNameAsync(string tagName)
    {
        try
        {
            var products = await _identityContext.Products.Include("Tag").Where(x => x.Tag.Name == tagName).ToListAsync();
            return products;
        }
        catch { return null!; }
    }
    public async Task<bool> RemoveProductAsync(ProductEntity entity)
    {
        try
        {   
            _identityContext.Products.Remove(entity);
            await _identityContext.SaveChangesAsync();
            return true;
        }
        catch { }
        return false;
    }
}
