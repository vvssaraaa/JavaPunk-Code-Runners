using Microsoft.AspNetCore.Mvc;
using Javapunk.Models;
using Javapunk.ViewModels;
using Javapunk.Data;

namespace Javapunk.Controllers;

public class QuizController : Controller{
    
    private readonly QuizService _quizService;
    private readonly UserService _userService;

    public QuizController(QuizService quizService, UserService userService){
        _quizService = quizService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Start(int moduleId, int userId){
        var questions = await _quizService.GetQuizQuestionsAsync(moduleId);

        if(questions.Count == 0){
            return NotFound("No questions found for this module");
        }
        string questionIds = string.Join(",", questions.Select(q => q.Id));

        return RedirectToAction("Question", new{questionIds, currentIndex = 0, score = 0, userId});
    }

    [HttpGet]
    public async Task<IActionResult> StartExam(int userId){

        var exam = await _quizService.CreateExamAsync();

        if(exam.Count == 0){
             return NotFound("No questions found for this exam");
        }

        string questionIds = string.Join(",", exam.Select(q => q.Id));

        return RedirectToAction("Question", new{questionIds, currentIndex = 0, score = 0, userId});
    }
 
    [HttpGet]
    public async Task<IActionResult> Question(string questionIds, int currentIndex, int score, int userId){
        List<int> questionIdList = questionIds .Split(",") .Select(int.Parse) .ToList();

        if(currentIndex >= questionIdList.Count){
            return RedirectToAction("Result",new {score, userId});
        }

        int currentId = questionIdList[currentIndex];

        var question = await _quizService.GetQuestionByIdAsync(currentId);

        if(question == null){
            return NotFound("Question was not found");
        }

        ViewBag.QuestionIds = questionIds;
        ViewBag.CurrentIndex = currentIndex;
        ViewBag.Score = score;
        ViewBag.UserId = userId;

        return View(question); 
    }
 
    [HttpPost]
    public async Task<IActionResult> Question(string questionIds, int currentIndex, int score, int userId, int answerId){
        List<int> questionIdList = questionIds .Split(",") .Select(int.Parse) .ToList();

        int currentQuestionId = questionIdList[currentIndex];

        bool correct = await _quizService.CheckAnswerAsync(currentQuestionId, answerId);

        if (correct){
            score += 10;
        }
        return RedirectToAction("Question", new {questionIds, currentIndex = currentIndex + 1, score, userId});
    }
 
    [HttpGet]
    public async Task<IActionResult> Result(int userId, int score){
        bool success = await _userService.AddScoreToUserScore(userId, score);

        if (!success){
            return NotFound("Could not save the user's score.");
        }
        ViewBag.UserId = userId;
        ViewBag.Score = score;

        return View();
    }
}
