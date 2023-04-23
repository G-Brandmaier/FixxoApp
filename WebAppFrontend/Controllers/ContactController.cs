using Microsoft.AspNetCore.Mvc;
using WebAppFrontend.ViewModels;

namespace WebAppFrontend.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync("https://localhost:7280/api/Contact?key=a98af364-4668-4613-b966-0f666c987e0b", model);//?key=a98af364-4668-4613-b966-0f666c987e0b
            if(result.IsSuccessStatusCode)
                return RedirectToAction("CommentSent", "Contact");
        }

        return View(model);
    }
    public IActionResult CommentSent()
    {
        return View();
    }

}
