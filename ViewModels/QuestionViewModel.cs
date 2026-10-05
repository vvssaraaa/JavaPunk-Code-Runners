using System.ComponentModel.DataAnnotations;
using Javapunk.Models;

namespace Javapunk.ViewModels
{
    public class QuestionViewModel
    {
        [Required]
        public int QuestionId { get; set; } = 0;
        [Required]
        public string QuestionText { get; set; } = "";
    }
}