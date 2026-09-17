using Microsoft.AspNetCore.Mvc;
using Javapunk.Models;
using Javapunk.ViewModels;
using Javapunk.Data;

namespace Javapunk.Controllers;
public class QuestionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuestionsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Questions question)
    {
        if (ModelState.IsValid)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
        return View(question);
    }

}