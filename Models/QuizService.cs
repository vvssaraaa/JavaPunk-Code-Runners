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

        public async Task<List<Questions>> GetQuestionsForModuleAsync(int moduleId, int questionCount = 10)
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
    }
}