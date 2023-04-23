using WebAppBackend.Helpers.Repositories;
using WebAppBackend.Models.Dtos;
using WebAppBackend.Models.Entites;

namespace WebAppBackend.Helpers.Services;

public class ProductService
{
    private readonly ProductRepo _productRepo;

    public ProductService(ProductRepo productRepo)
    {
        _productRepo = productRepo;
    }
    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        var products = await _productRepo.GetAllAsync();
        List<ProductModel> result = new List<ProductModel>();
        foreach (var item in products)
        {
            ProductModel model = item;
            result.Add(model);
        }
        return result;
    }
    public async Task<bool> CreateProductAsync(ProductModel model)
    {
        var tag = await _productRepo.GetTagbyNameAsync(model.TagName);
        ProductEntity entity = model;
        entity.Tag = tag;
        return await _productRepo.CreateProductAsync(entity);
    }

    public async Task<ProductModel> GetProductByIdAsync(string id)
    {
        var result = await _productRepo.GetProductByIdAsync(id);
        if (result != null)
            return result;

        return null!;
    }

    public async Task<TagEntity> GetTagByNameAsync(string name)
    {
        var result = await _productRepo.GetTagbyNameAsync(name);
        return result;
    }
    public async Task<bool> CreateTagAsync(TagModel model)
    {
        var result = await GetTagByNameAsync(model.Name);
        if (result == null)
        {
            TagEntity entity = model;
            var tagResult = await _productRepo.AddTagAsync(model);

            return tagResult;
        }

        return false;
    }
    public async Task<IEnumerable<ProductModel>> GetProductsByTagNameAsync(string tagName)
    {
        try
        {
            var products = await _productRepo.GetProductsByTagNameAsync(tagName);
            List<ProductModel> result = new List<ProductModel>();
            foreach (var item in products)
            {
                ProductModel model = item;
                result.Add(model);
            }

            return result;
        }
        catch { }
        return null!;
    }

    public async Task<bool> RemoveProductAsync(ProductModel model)
    {
        ProductEntity entity = model;
        TagEntity tagEntity = await GetTagByNameAsync(model.TagName);
        entity.Tag = tagEntity;
        return await _productRepo.RemoveProductAsync(entity);
    }
}
