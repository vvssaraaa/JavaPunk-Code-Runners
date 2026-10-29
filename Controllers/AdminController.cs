using Microsoft.AspNetCore.Mvc;

namespace Javapunk.Controllers;

public class AdminController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string password)
    {
        if (password == "admin123")
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Index", "AdminModules");
        }

        ViewBag.Error = "Incorrect password. Please try again.";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("IsAdmin");
        return RedirectToAction("Index", "Modules");
    }
}