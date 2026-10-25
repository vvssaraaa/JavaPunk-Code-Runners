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
    public async Task<ActionResult> Index(int userId = 0)
    {

        ViewBag.UserId = userId;
        ViewBag.IsAdmin = IsAdmin();

        var modules = await _context.Modules.ToListAsync();
        return View(modules);
    }
    public IActionResult AdminView()
    {
        ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin") == "true";
        return View();
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

    //Endre på moduler
    public async Task<IActionResult> EditList()
    {
        var modules = await _context.Modules.ToListAsync();
        return View(modules);
    }

    public IActionResult Edit(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var module = _context.Modules
            .Where(m => m.Id == id)
            .ToList().FirstOrDefault();

        if (module == null)
            return NotFound();

        return View(module);
    }

    //Disse funksjonene er for å slette moduler fra databasen
    //Her returneres et view så admin kan velge hvilken modul som skal slettes
    [HttpGet]
    public async Task<IActionResult> DeleteIndex()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var modules = await _context.Modules.ToListAsync();
        return View(modules);
    }

    //Her slettes modulen fra databasen, basert på id
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var moduleForDeletion = await _context.Modules.FindAsync(id)
            ?? throw new Exception($"Module with ID {id} does not exist.");

        _context.Modules.Remove(moduleForDeletion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(DeleteIndex));
    }
}