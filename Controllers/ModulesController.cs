using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Javapunk.Data;
using Javapunk.Models;

namespace Javapunk.Controllers;

public class ModulesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ModulesController(ApplicationDbContext context)
    {
        _context = context;
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
        var modules = await _context.Modules.ToListAsync();

        ViewBag.UserId = userId;

        return View(modules);
    }

    public IActionResult Create()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        return View();
    }

    [HttpPost]
    
    public async Task<IActionResult> Create(Modules module)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        if (!ModelState.IsValid)
            return View(module);

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}