using Microsoft.EntityFrameworkCore;
namespace Javapunk.Models{

    // Handles quiz logic for loading questions, checking answers, and creating new questions.
    public class QuizService{

        private readonly Javapunk.Data.ApplicationDbContext _context;
        private readonly ILogger<QuizService> _logger;
        private readonly Random _random = new(); //brukes til å lage tilfeldig rekkefølge

        public QuizService(Javapunk.Data.ApplicationDbContext context, ILogger <QuizService> logger){
            _context = context;
            _logger = logger;
        }
        // Picks a random set of questions and their answers from a module.
        public async Task<List<Questions>> GetQuizQuestionsAsync(int moduleId, int questionCount = 10){ 
            try{
        
            var questions = await _context.Questions
            .Where(q => q.Modules.Id == moduleId)
            .Include(q => q.Answers)
            .ToListAsync();

            var selectedQuestions = questions
            .OrderBy(_ => _random.Next())
            .Take(Math.Min(questionCount, questions.Count))
            .ToList();
        
            foreach(var question in selectedQuestions){
                question.Answers = question.Answers
                .OrderBy(_ => _random.Next())
                .ToList();
            } 
            return selectedQuestions;
           }
           catch (Exception e){
            _logger.LogError(e, "Failed to load quiz questions");
            throw;
           }
        }
        // Loads one question by ID, including its answers.
        public async Task<Questions?> GetQuestionByIdAsync(int questionId){
        try{
           return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }
        catch(Exception e){
                _logger.LogError(e, "Failed to load question");
                return null;
            } 
        }       
        // Checks whether a given answer is the correct one for a specificquestion.
        public async Task<bool> CheckAnswerAsync(int questionId, int answerId){
            var question = await GetQuestionByIdAsync(questionId);
            
            if (question == null){
             return false;
            }
            
            var answer = question.Answers
            .FirstOrDefault(a => a.Id == answerId);
            
            return answer?.Is_correct ?? false;
        }
         // Picks a random set of questions from ALL modules (used for exam mode).
        public async Task<List<Questions>> CreateExamAsync(int questionCount = 10){
            try{
            var questions = await _context.Questions
            .Include(q => q.Answers)
            .ToListAsync();

            var selectedQuestions = questions
            .OrderBy(_ => _random.Next())
            .Take(Math.Min(questionCount, questions.Count))
            .ToList();

            foreach(var question in selectedQuestions){
                question.Answers = question.Answers
                .OrderBy(_ => _random.Next())
                .ToList();
            }

            return selectedQuestions;
        }
        catch(Exception e){
            _logger.LogError(e, "Failed to load exam questions");
            throw;
        }
    }
        // Validates input, then creates and saves a new question with its answers.
        public async Task<Questions?> CreateNewQuestion(string questionText, List<string> answerOptions, int correctAnswerIndex, int moduleId){
            if(string.IsNullOrWhiteSpace(questionText)){
                return null;
            }
            if(answerOptions.Count < 2){
                return null;
            }
            if(correctAnswerIndex < 0 || correctAnswerIndex >= answerOptions.Count){
                return null;
            }
           
            try{
            Modules? module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == moduleId);
            if(module == null){
                return null;
            }

            Questions question = new Questions();
            question.Question_text = questionText;
            question.Modules = module;

            foreach(string answerText in answerOptions){
                Answers answer = new Answers();
                answer.Answer_text = answerText;
                answer.Is_correct = answerOptions.IndexOf(answerText) == correctAnswerIndex;
                answer.Questions = question;

                question.Answers.Add(answer);
            }    
            _context.Add(question);
            
            await _context.SaveChangesAsync();

            return question;
            }
            catch(Exception e){
                _logger.LogError(e, "Failed to create new question");
                return null;
            }
        }
     }
   }