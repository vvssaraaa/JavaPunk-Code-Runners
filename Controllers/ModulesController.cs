using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Javapunk.Data;

namespace Javapunk.Controllers;

public class ModulesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ModulesController(ApplicationDbContext context)
    {
        _context = context;
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
}