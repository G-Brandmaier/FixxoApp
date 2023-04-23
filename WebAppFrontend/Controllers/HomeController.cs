using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebAppFrontend.Models;
using WebAppFrontend.ViewModels;

namespace WebAppFrontend.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        using var http = new HttpClient();

        var latestShowcase = await http.GetAsync("https://localhost:7280/api/Showcase?key=a98af364-4668-4613-b966-0f666c987e0b");
        
        var token = HttpContext.Request.Cookies["jwtToken"];
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var tagNewResult = await http.GetAsync("https://localhost:7280/api/Product/GetProducts/" + "New" + "?key=a98af364-4668-4613-b966-0f666c987e0b");
        var tagPopularResult = await http.GetAsync("https://localhost:7280/api/Product/GetProducts/" + "Popular" + "?key=a98af364-4668-4613-b966-0f666c987e0b");
        var tagFeaturedResult = await http.GetAsync("https://localhost:7280/api/Product/GetProducts/" + "Featured" + "?key=a98af364-4668-4613-b966-0f666c987e0b");

        if (latestShowcase != null)
        {
            var showcaseResult = await latestShowcase.Content.ReadFromJsonAsync<ShowcaseModel>();

            var newProducts = await tagNewResult.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
            var  tagNew = newProducts!.Select(x =>x).Take(8).ToList();

            var popularProducts = await tagPopularResult.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
            var tagPopular = popularProducts!.Select(x => x).Take(8).ToList();

            var featuredProducts = await tagFeaturedResult.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
            var tagFeatured = featuredProducts!.Select(x => x).Take(8).ToList();


            HomeIndexViewModel model = new HomeIndexViewModel
            {
                Showcase = showcaseResult!,
                TagCollection = {
                    new TagCollectionModel
                    {
                        TagName = "New Products",
                        Products = tagNew
                    },
                    new TagCollectionModel
                    {
                        TagName = "Popular Products",
                        Products = tagPopular
                    },
                    new TagCollectionModel
                    {
                        TagName = "Featured Products",
                        Products = tagFeatured
                    }
                }
            };
            return View(model);
        }
        return View();
    }
}
