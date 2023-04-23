using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebAppFrontend.Models;
using WebAppFrontend.ViewModels;

namespace WebAppFrontend.Controllers;

public class ProductsController : Controller
{
    public async Task<IActionResult> Index()
    {
        using var http = new HttpClient();
        var allProducts = await http.GetAsync("https://localhost:7280/api/Product/AllProducts?key=a98af364-4668-4613-b966-0f666c987e0b");

        var productsResult = await allProducts.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
        return View(productsResult);
    }
    public IActionResult RegisterProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterProduct(RegisterNewProductViewModel model)
    {
        if(ModelState.IsValid)
        {
            using var http = new HttpClient();
            var token = HttpContext.Request.Cookies["jwtToken"];
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await http.PostAsJsonAsync("https://localhost:7280/api/Product/AddNewProduct?key=a98af364-4668-4613-b966-0f666c987e0b", model);

            if (result.IsSuccessStatusCode) 
                return RedirectToAction("RegistrationSucceded", "Products");

            ModelState.AddModelError("", "Something went wrong trying to register new product");
        }
        return View(model);
    }
    public IActionResult RegistrationSucceded()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ProductDetail(string articleNumber)
    {
        using var http = new HttpClient();
        var result = await http.GetAsync("https://localhost:7280/api/Product/" + articleNumber + "?key=a98af364-4668-4613-b966-0f666c987e0b");
        var product = await result.Content.ReadFromJsonAsync<ProductModel>();

        if (result.IsSuccessStatusCode)
        {
            return View(product);
        }
        return Redirect("Index");
    }


    public async Task<IActionResult> GetAllProductsForEdit()
    {
        using var http = new HttpClient();
        var allProducts = await http.GetAsync("https://localhost:7280/api/Product/AllProducts?key=a98af364-4668-4613-b966-0f666c987e0b");

        var productsResult = await allProducts.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
        return View(productsResult);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(string articleNumber)
    {
        using var http = new HttpClient();
        var result = await http.GetAsync("https://localhost:7280/api/Product/" + articleNumber + "?key=a98af364-4668-4613-b966-0f666c987e0b");
        var product = await result.Content.ReadFromJsonAsync<ProductModel>();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(ProductModel model)
    {
        if(ModelState.IsValid)
        {
            using var http = new HttpClient();
            var token = HttpContext.Request.Cookies["jwtToken"];
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await http.PostAsJsonAsync("https://localhost:7280/api/Product/RemoveProduct?key=a98af364-4668-4613-b966-0f666c987e0b", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("DeleteSucceded", "Products");
        }

        return RedirectToAction("GetAllProductsForEdit", "Products");
    }

    public IActionResult DeleteSucceded()
    {
        return View();
    }
}
