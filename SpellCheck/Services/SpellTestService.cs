using SpellCheck.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellCheck.Services
{
    public class SpellTestService : ISpellTestService
    {

        Random rnd = new Random();

        private List<SpellingViewModel> Questions { get; set; }

        private SpellingViewModel _current;

        private ISpeachService _speachService;


        public SpellTestService(List<SpellingViewModel> _questions, ISpeachService speachService)
        {
            Questions = _questions;
            _speachService = speachService;
        }

        public SpellingViewModel NextQuestion()
        {
            var unanswered = Questions.Where(q => q.CorrectCount < 1 && !q.Skipped);
            if (unanswered.Count() == 0)
            {
                return null;
            }
            int i = rnd.Next(0, unanswered.Count());
            _current = unanswered.ElementAt(i);
            return _current;
        }

        public SpellingViewModel AnswerQuestion(string answer)
        {
            if (answer.ToUpper() == _current.Word.ToUpper())
            {
                _current.CorrectCount += 1;
                _speachService.Say("Correct.", false);
            } 
            else
            {
                _current.ErrorCount += 1;
                _speachService.Say("Wrong.", false);
            }
            return NextQuestion();
        }

        public SpellingViewModel SkipQuestion()
        {
            _current.Skipped = true;
            return NextQuestion();
        }


    }

 
    public enum QuestionResult
    {
        CORRECT,
        WRONG,
        SKIPPED
    };

}
