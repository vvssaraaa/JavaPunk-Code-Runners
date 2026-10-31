using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Javapunk.Data;
using Javapunk.Models;

namespace Javapunk.Controllers;

public class AdminModulesController : Controller
{
    private readonly ApplicationDbContext _context;
    public AdminModulesController(ApplicationDbContext context)
    {
        _context = context;
    }
    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("IsAdmin") == "true";
    }

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var modules = await _context.Modules.ToListAsync();
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

        if (ModelState.IsValid)
        {
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(module);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var module = await _context.Modules.FindAsync(id);
        if (module == null)
        {
            return NotFound();
        }
        return View(module);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Modules module)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        if (id != module.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(module);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var module = await _context.Modules.FindAsync(id);
        if (module == null)
            return NotFound();

        return View(module);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!IsAdmin())
            return RedirectToAction("Login", "Admin");

        var module = await _context.Modules.FindAsync(id);
        if (module == null)
            return NotFound();

        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}