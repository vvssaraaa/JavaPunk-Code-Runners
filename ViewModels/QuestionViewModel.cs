using Javapunk.Models;

namespace Javapunk.ViewModels
{
    public class QuestionsViewModel
    {
        public IEnumerable<Questions> Questions;
        public String? CurrentViewName;

        public QuestionsViewModel(IEnumerable<Questions> questions, String? currentViewName)
        {
            Questions = questions;
            CurrentViewName = currentViewName;
        }
    }
}

