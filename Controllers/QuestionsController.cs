using Microsoft.AspNetCore.Mvc;
using Javapunk.Models;
using Javapunk.ViewModels;
using Javapunk.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using System.Numerics;
using System.Diagnostics;

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
        return View();
    }
    
    public IActionResult Create()
    {
        return View();
    }

    // Loads question based on id, and returns view with loaded questions data (including answers and modules)
    public IActionResult EditQuestion(int id)
    {
        var question = _context.Questions
        .Where(q => q.Id == id)
        .Include(q => q.Answers)
        .Include(q => q.Modules)
        .ToList().First();
        return View(CreateQuestionViewModel.toDomain(question));
    }

    // Function to update question data based on given values in viewModel
    [HttpPost]
    public async Task<IActionResult> Edit(int id, CreateQuestionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var modules = await _context.Modules.ToListAsync();
        var module = modules.Find(module => module.Id == viewModel.Modules) 
        ?? throw new Exception($"A module with id {viewModel.Modules} does not exist");
        
        // We need to load answers and modules here since they can also be edited
        var question = _context.Questions
        .Include(q => q.Answers)
        .Include(q => q.Modules)
        .Where(q => q.Id == id).ToList().First();

        // Here we clear answers, before saving the new values
        question.Answers.Clear();
            var answers = new List<Answers>
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
            };
        question.Answers = answers;
        question.Question_text = viewModel.QuestionText;
        question.Modules = module;

        _context.Questions.Update(question);
        _context.SaveChanges();
        return RedirectToAction(nameof(EditList));
    }

    // This function loads all questions, and returns a view where the user can select which ones to edit
    public async Task<IActionResult> EditList()
    {
        var questions = await _context.Questions.ToListAsync();
        var questionsForEditing = questions.Select(question => new QuestionViewModel() { QuestionId = question.Id, QuestionText = question.Question_text}).ToList();
        return View(questionsForEditing);
    }

    // This function creates a new question, based on the given values in viewModel
    [HttpPost]
    public async Task<IActionResult> Create(CreateQuestionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var modules = await _context.Modules.ToListAsync();
        var module = modules.Find(module => module.Id == viewModel.Modules) 
        ?? throw new Exception($"A module with id {viewModel.Modules} does not exist");
        var question = new Questions
        {
            Question_text = viewModel.QuestionText,
            Modules =  module,
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
        return View();
        }

    // This function loads all questions, and returns a view where the user can select which ones to delete
    [HttpGet]
    public async Task<IActionResult> DeleteIndex()
    {
        var questions = await _context.Questions.ToListAsync();
        var questionsForDeletion = questions.Select(question => new QuestionViewModel() { QuestionId = question.Id, QuestionText = question.Question_text}).ToList();
        return View(questionsForDeletion);
    }

    // This function deletes a question based on the given id, and also removes the answers for that question
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var questionForDeletion = _context.Questions.Find(id)?? throw new Exception($"Question with id {id} does not exist");
        questionForDeletion.Answers.Clear();
        _context.Questions.Remove(questionForDeletion);
        _context.SaveChanges(); 
        return RedirectToAction(nameof(DeleteIndex));
    }
}
