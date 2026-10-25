using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Javapunk.Models;

namespace Javapunk.Controllers;

public class MenuController : Controller
{
    public IActionResult Index()
    {
        ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin") == "true";
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}