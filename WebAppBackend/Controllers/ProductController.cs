using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Filters;
using WebAppBackend.Helpers.Services;
using WebAppBackend.Models.Dtos;

namespace WebAppBackend.Controllers;

[UseApiKey]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Route("AllProducts")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        if(products != null) 
            return Ok(products);

        return NotFound();
    }

    [HttpGet("GetProducts/{tagName}")]
    public async Task<IActionResult> GetAllByTagName(string tagName)
    {
        var products = await _productService.GetProductsByTagNameAsync(tagName);
        if(products != null)
            return Ok(products);

        return NotFound();
    }


    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(string productId)
    {
         var result = await _productService.GetProductByIdAsync(productId);
        if(result != null)
            return Ok(result);

        return BadRequest();
    }

    [Route("CreateTag")]
    [HttpPost]
    public async Task<IActionResult> CreateTag(TagModel model)
    {
        var result = await _productService.CreateTagAsync(model);
        if (result)
            return Created("", null);

        return BadRequest(model);
    }

    [Authorize(Roles = "admin, product manager")]
    [Route("AddNewProduct")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductModel model)
    {
        var result = await _productService.CreateProductAsync(model);
        if (result)
            return Created("", null);

        return BadRequest(model);
    }

    [Authorize(Roles = "admin, product manager")]
    [Route("RemoveProduct")] 
    [HttpPost] 
    public async Task<IActionResult> RemoveProduct(ProductModel model)
    {
        var result = await _productService.RemoveProductAsync(model);
        if (result)
            return Ok(true);

        return BadRequest();
    }
}
