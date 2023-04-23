using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebAppFrontend.Models;
using WebAppFrontend.ViewModels;

namespace WebAppFrontend.Controllers;

public class AccountController : Controller
{

    #region Register
    public async Task< IActionResult> Register()
    {
        using var http = new HttpClient();
        var token = HttpContext.Request.Cookies["jwtToken"];
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var checkUserLogedIn = await http.GetAsync("https://localhost:7280/api/Account/CheckLogedIn?key=a98af364-4668-4613-b966-0f666c987e0b");
        var logedIn = await checkUserLogedIn.Content.ReadFromJsonAsync<bool>();
        if (logedIn)
            return RedirectToAction("MyAccount", "Account");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync("https://localhost:7280/api/Account/Register?key=a98af364-4668-4613-b966-0f666c987e0b", model);
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Login", "Account");
        }
        return View(model);
    }
    #endregion

    #region Login & Logut
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(ModelState.IsValid)
        {
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync("https://localhost:7280/api/Account/Login?key=a98af364-4668-4613-b966-0f666c987e0b", model);
            if(result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddHours(1)
                    });

                    return RedirectToAction("MyAccount", "Account");
                }
            }
            ModelState.AddModelError("", "Incorrect email or password");
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        try
        {
            HttpContext.Response.Cookies.Delete("jwtToken");

            return RedirectToAction("Index", "Home");
        }
        catch { }

        return RedirectToAction("MyAccount", "Account");
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> MyAccount()
    {
        using var http = new HttpClient();
        var token = HttpContext.Request.Cookies["jwtToken"];
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await http.GetFromJsonAsync<UserModel>("https://localhost:7280/api/Account/MyAccount?key=a98af364-4668-4613-b966-0f666c987e0b");

        if (result != null)
            return View(result);


        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> CheckLogedIn()
    {
        using var http = new HttpClient();
        var token = HttpContext.Request.Cookies["jwtToken"];
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await http.GetAsync("https://localhost:7280/api/Account/CheckLogedIn?key=a98af364-4668-4613-b966-0f666c987e0b");

        var logedIn = await result.Content.ReadFromJsonAsync<bool>();
        if (logedIn)
            return RedirectToAction("MyAccount", "Account");

        return RedirectToAction("Login", "Account");
    }
}
