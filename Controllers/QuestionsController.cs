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
    public IActionResult Index()
    {
        List<Questions> questions = _context.Questions.ToList();
        var questionViewModel = new QuestionsViewModel(questions, "Questions");
        return View(questionViewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateQuestionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var question = new Questions
        {
            Question_text = viewModel.QuestionText,
            //Har satt modules til null for å unngå feil, men dette bør endres når moduler er implementert
            Modules = null,
            Answers = new List<Answers>
            {
                new Answers
                {
                    Answer_text = viewModel.CorrectAnswer,
                    Is_correct = true
                },
                new Answers
                {
                    Answer_text = viewModel.WrongAnswer1,
                    Is_correct = false
                },
                new Answers
                {
                    Answer_text = viewModel.WrongAnswer2,
                    Is_correct = false
                },
                new Answers
                {
                    Answer_text = viewModel.WrongAnswer3,
                    Is_correct = false
                }
            }
        };

        _context.Questions.Add(question);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
        }
    }
