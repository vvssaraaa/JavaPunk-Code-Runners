using Javapunk.Data;
using Javapunk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Javapunk.Tests;

// Unit tests for QuizService using an isolated in-memory database.
public class QuizServiceTests{
    private static ApplicationDbContext CreateContext(){

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    // Positive: gets the questions for an existing module
    [Fact]
    public async Task GetQuizQuestions_ValidModule_ReturnsQuestions()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);


        var module = new Modules {Module_name = "TestModule"};
        context.Modules.Add(module);

        var question = new Questions{Question_text = "What is an int", Modules = module};
        context.Questions.Add(question);
        await context.SaveChangesAsync();

        var result = await service.GetQuizQuestionsAsync(module.Id);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("What is an int", result[0].Question_text);
    }

    // Positive: returns only questions belonging to the requested module
    [Fact]
    public async Task GetQuizQuestions_ValidModule_ReturnsOnlyRequestedModule()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var moduleA = new Modules {Module_name = "TestModule A"};
        var moduleB = new Modules {Module_name = "TestModule B"};

        context.Modules.Add(moduleA);
        context.Modules.Add(moduleB);

        var questionA = new Questions{Question_text = "What is an int", Modules = moduleA};
        var questionB = new Questions{Question_text = "What is a string", Modules = moduleB};

        context.Questions.Add(questionA);
        context.Questions.Add(questionB);
        await context.SaveChangesAsync();

        var result = await service.GetQuizQuestionsAsync(moduleA.Id);

        Assert.Single(result);
        Assert.Contains(result, q => q.Question_text == "What is an int");
        Assert.DoesNotContain(result, q => q.Question_text == "What is a string");
    }

    // Positive: returns true when the selected answer is correct
    [Fact]
    public async Task CheckAnswer_CorrectAnswer_ReturnsTrue()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var question = new Questions {Question_text = "What is an int"};
        context.Questions.Add(question);

        var answer = new Answers {Answer_text = "A data type", Is_correct = true, Questions = question};
        question.Answers.Add(answer);
        await context.SaveChangesAsync();

        var result = await service.CheckAnswerAsync(question.Id, answer.Id);

        Assert.True(result);
    }
    
    // Negative: returns false when the selected answer is incorrect
    [Fact]
    public async Task CheckAnswer_WrongAnswer_ReturnsFalse()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var question = new Questions {Question_text = "What is an int"};
        context.Questions.Add(question);

        var correctAnswer = new Answers{Answer_text = "A data type", Is_correct = true, Questions = question};
        var wrongAnswer = new Answers {Answer_text = "A sock", Is_correct = false, Questions = question};

        question.Answers.Add(correctAnswer);
        question.Answers.Add(wrongAnswer);
        await context.SaveChangesAsync();

        var result = await service.CheckAnswerAsync(question.Id, wrongAnswer.Id);

        Assert.False(result);
    }

    // Positive: creates and saves a question with valid input
    [Fact]
    public async Task CreateNewQuestion_ValidInput_SavesQuestion()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var module = new Modules {Module_name = "TestModule"};
        context.Modules.Add(module);
        await context.SaveChangesAsync();

        var result = await service.CreateNewQuestion("What is a string", new List<string>{"A data type", "A cat"},0, module.Id);

        Assert.NotNull(result);
        Assert.Equal("What is a string", result.Question_text);
        Assert.Equal(2, result.Answers.Count);
        Assert.Contains(result.Answers, a => a.Answer_text == "A data type" && a.Is_correct);
        Assert.Contains(result.Answers, a => a.Answer_text == "A cat" && !a.Is_correct);

        var savedQuestion = await context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Question_text == "What is a string");
        Assert.NotNull(savedQuestion);
        Assert.Equal(2, savedQuestion.Answers.Count);
    }

    // Negative: rejects a question if question text is empty
    [Fact]
    public async Task CreateNewQuestion_EmptyText_RejectsQuestion()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var module = new Modules {Module_name = "TestModule"};
        context.Modules.Add(module);
        await context.SaveChangesAsync();

        var resultEmptyText = await service.CreateNewQuestion("", new List<string>{"A data type", "A dog"},0, module.Id);

        Assert.Null(resultEmptyText);
        Assert.Empty(context.Questions);
    }

    // Negative: rejects a question if it has fewer than two answer options
    [Fact]
    public async Task CreateNewQuestion_InsufficientAnswers_RejectsQuestion()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var module = new Modules {Module_name = "TestModule"};
        context.Modules.Add(module);
        await context.SaveChangesAsync();

        var newQuestion = await service.CreateNewQuestion("What is an int", new List<string>{"A data type"}, 0, module.Id);

        Assert.Null(newQuestion);
        Assert.Empty(context.Questions);
    }

    // Negative: rejects a question if the specified module does not exist
    [Fact]
    public async Task CreateNewQuestion_NonexistentModule_RejectsQuestion()
    {
        await using var context = CreateContext();
        var logger = NullLogger<QuizService>.Instance;
        var service = new QuizService(context, logger);

        var nonExistentModuleId = 1234;

        var newQuestion = await service.CreateNewQuestion("what is an int", new List<string>{"A data type", "A monkey"},0, nonExistentModuleId);

        Assert.Null(newQuestion);
        Assert.Empty(context.Questions);
    }
}