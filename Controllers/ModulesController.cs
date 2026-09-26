using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Javapunk.Data;

namespace Javapunk.Controllers;

public class ModulesController : Controller{
    private readonly ApplicationDbContext _context;

    public ModulesController(ApplicationDbContext context){
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult> Index(int userId)
    {
        var modules = await _context.Modules.ToListAsync();

        ViewBag.UserId = userId;

        return View(modules);
    }
}