using System.ComponentModel.DataAnnotations;

namespace Javapunk.ViewModels
{
   public class CreateQuestionViewModel
    {
       [Required]
       public string QuestionText { get; set; } ="";

       [Required]
       public string CorrectAnswer { get; set; } = "";

       [Required]
       public string WrongAnswer1 { get; set; } = "";

       [Required]
       public string WrongAnswer2 { get; set; } = "";

       [Required]
       public string WrongAnswer3 { get; set; } = "";
    }
}