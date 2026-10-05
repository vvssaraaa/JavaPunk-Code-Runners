using System.ComponentModel.DataAnnotations;
using Javapunk.Models;

namespace Javapunk.ViewModels
{
    public class CreateQuestionViewModel
    {
        [Required]
        public int Modules { get; set; } = 0;
        public int? QuestionId { get; set; } = null;

        [Required]
        public string QuestionText { get; set; } = "";

        [Required]
        public string CorrectAnswer { get; set; } = "";

        [Required]
        public string WrongAnswer1 { get; set; } = "";

        [Required]
        public string WrongAnswer2 { get; set; } = "";

        [Required]
        public string WrongAnswer3 { get; set; } = "";

        public static CreateQuestionViewModel toDomain(Questions questions)
        {
            var wrongAnswers = questions.Answers.Where(it => it.Is_correct == false).ToArray();
            return new CreateQuestionViewModel
            {
                Modules = questions.Modules!.Id,
                QuestionId = questions.Id,
                QuestionText = questions.Question_text,
                CorrectAnswer = questions.Answers.First(it => it.Is_correct == true).Answer_text,
                WrongAnswer1 = wrongAnswers[0].Answer_text,
                WrongAnswer2 = wrongAnswers[1].Answer_text,
                WrongAnswer3 = wrongAnswers[2].Answer_text
            };
        }
    }
}