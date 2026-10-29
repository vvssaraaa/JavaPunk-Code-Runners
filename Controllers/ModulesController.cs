using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Javapunk.Data;
using Javapunk.Models;

namespace Javapunk.Controllers;

public class ModulesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserService _userService;

    public ModulesController(ApplicationDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }

    //Hjelpemetode for å sjekke om brukeren er logget inn som admin
    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("IsAdmin") == "true";
    }
    public IActionResult Index()
    {
        return View();
    }
    // Loads all modules from the database and passes them to the view, along with the current user's ID
    [HttpGet]
    public async Task<ActionResult> Index(int userId)
    {

        ViewBag.UserId = userId;
        ViewBag.IsAdmin = IsAdmin();

        var modules = await _context.Modules.ToListAsync();
        var lastCompletedModule = await _userService.GetLastCompleted(userId);
        ViewBag.LastCompletedModule = lastCompletedModule?.Modules?.Id ?? 0;
        return View(modules);
    }
    public IActionResult AdminView()
    {
        ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin") == "true";
        return View();
    }
}