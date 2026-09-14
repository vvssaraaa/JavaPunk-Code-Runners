namespace CodeRunner.Services // Grupperer koden med andre deler av programmet
{
    // Foreløpig kun core-logikk.
    // Representerer et spørsmål og svarene som hører til spørsmålet.
    public class Question
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty; // Spørsmålet som vises til spilleren
        public List<Answer> Answers { get; set; } = new(); // Lagrer svaralternativene til spørsmålet
    }

    public class Answer
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty; // Teksten til svaralternativet
        public bool IsCorrect { get; set; } // Sier om svaret er riktig eller ikke
    }

    // Enkel quiz-logikk: velger spørsmål, går gjennom dem og holder styr på score.
    // Foreløpig kun core-logikk og ikke koblet til databasen.
    public class QuizService
    {
        private readonly List<Question> _questions; // Lagrer spørsmålene som brukes i denne quiz-runden
        private int _currentIndex; // Lagrer hvilket spørsmål spilleren er på nå

        // Lagrer poengsummen. Private set betyr at bare QuizService kan endre scoren.
        public int Score { get; private set; }

        // Sjekker om spilleren har svart på alle spørsmålene.
        public bool IsFinished => _currentIndex >= _questions.Count;

        public QuizService(List<Question> allQuestions, int questionCount = 10)
        {
            var random = new Random();

            // Stokker spørsmålene slik at de kommer i tilfeldig rekkefølge.
            // Dette kan endres senere når vi legger til for eksempel boss-spørsmål.
            _questions = allQuestions
                .OrderBy(_ => random.Next())
                .Take(Math.Min(questionCount, allQuestions.Count))
                .ToList();

            _currentIndex = 0;
            Score = 0;

            // Starter quiz-runden på første spørsmål med 0 poeng.
        }

        // Henter spørsmålet som spilleren skal svare på.
        // Returnerer null hvis quiz-runden er ferdig.
        public Question? GetCurrentQuestion()
        {
            return IsFinished ? null : _questions[_currentIndex];
        }

        // Sjekker svaret spilleren har valgt.
        // Returnerer true hvis svaret er riktig, og false hvis det er feil.
        public bool SubmitAnswer(long answerId)
        {
            var current = GetCurrentQuestion();

            // Hindrer spilleren fra å svare når quiz-runden allerede er ferdig.
            if (current is null)
                throw new InvalidOperationException("Quiz is already finished.");

            // Finner svaret spilleren valgte blant svarene til det aktuelle spørsmålet.
            var chosen = current.Answers.FirstOrDefault(a => a.Id == answerId);

            // Sjekker om svaret finnes og om det er markert som riktig.
            bool wasCorrect = chosen is not null && chosen.IsCorrect;

            // Hvis svaret er riktig, får spilleren 10 poeng.
            // Kan endres senere hvis vi legger til mer avansert poengberegning.
            if (wasCorrect)
                Score += 10;

            // Går videre til neste spørsmål.
            _currentIndex++;

            // Returnerer om spilleren svarte riktig eller feil.
            return wasCorrect;
        }
    }
}