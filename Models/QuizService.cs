using Microsoft.EntityFrameworkCore;
namespace Javapunk.Models
{
    public class QuizService
    {
        private readonly Javapunk.Data.ApplicationDbContext _context;
        private readonly Random _random = new(); //brukes til å lage tilfeldig rekkefølge

        private List<Questions> _questions = new();
        private int _currentIndex = 0;

        public int Score {get; private set;} 

        public QuizService(Javapunk.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Questions>> GetQuizQuestionsAsync(int moduleId, int questionCount = 10)
        { 
            //henter spørsmålene til den valgte modulen og svarene som tilhører
            var questions = await _context.Questions
            .Where(q => q.Modules.Id == moduleId)
            .Include(q => q.Answers)
            .ToListAsync();


            _questions = questions
            .OrderBy(_ => _random.Next())
            .Take(Math.Min(questionCount, questions.Count))
            .ToList();
            //stokker om spørsmålene og velger max 10

            _currentIndex = 0;
            Score = 0;

            foreach(var question in _questions){
                question.Answers = question.Answers
                .OrderBy(_ => _random.Next())
                .ToList();
            } //stokker også svarene slik at de også kommer i tilfeldig rekkefølge, ikke samme hver gang

            return _questions;
           }

        //simple eksamen run som plukker tilfeldig ut 10 spørsmål fra modulene, stokker om. 
        public async Task<List<Questions>> CreateExamAsync(int questionCount = 10){
            var questions = await _context.Questions
            .Include(q => q.Answers)
            .ToListAsync();

            _questions = questions
            .OrderBy(_ => _random.Next())
            .Take(Math.Min(questionCount, questions.Count))
            .ToList();

            foreach(var question in _questions){
                question.Answers = question.Answers
                .OrderBy(_ => _random.Next())
                .ToList();
            } //stokker også svarene slik at de også kommer i tilfeldig rekkefølge, ikke samme hver gang

            _currentIndex = 0;
            Score = 0;
            //reset scoren for eksamen
            return _questions;
        }

        public bool IsFinished => _currentIndex >= _questions.Count;

        public Questions? GetCurrentQuestion()
        {
            return IsFinished ? null : _questions[_currentIndex];
        }

        public bool SubmitAnswer(int answerId)
        {
            var current = GetCurrentQuestion();

            if (current is null)
                throw new InvalidOperationException("Quiz is already finished.");

            var chosen = current.Answers.FirstOrDefault(a => a.Id == answerId);
            bool wasCorrect = chosen is not null && chosen.Is_correct;
            //finner svaret spilleren valgte og sjekker om det var riktig

            if (wasCorrect)
                Score += 10;

            _currentIndex++;

            return wasCorrect;
        }
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
            //validering som passer på at spørsmålteksten ikke er tom, at det er minst 2 svar og at index for det riktige alternative ikke 
            //overskreder antall svar det faktisk er. 
            
            Modules? module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == moduleId);
            if(module == null){
                return null;
            } //Henter modulen fra databasen med riktig Id, hvis modulen ikke finnes blir module null

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
            //lager objekter for answer og question, setter at svarene tilhører et spesifikt spørsmål. 
    
            _context.Add(question);
            
            await _context.SaveChangesAsync();

            return question;
        }
    }
}