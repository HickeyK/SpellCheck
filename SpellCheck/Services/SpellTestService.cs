using SpellCheck.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellCheck.Services
{
    class SpellTestService
    {

        Random rnd = new Random();

        public List<SpellingViewModel> Questions { get; set; }

        private SpellingViewModel _current;


        public SpellTestService(List<SpellingViewModel> _questions)
        {
            Questions = _questions;
        }

        public SpellingViewModel NextQuestion()
        {
            var unanswered = Questions.Where(q => q.CorrectCount < 1);
            if (unanswered.Count() == 0)
            {
                return null;
            }
            int i = rnd.Next(0, unanswered.Count() - 1);
            _current = unanswered.ElementAt(i);
            return _current;
        }

        public SpellingViewModel AnswerQuestion(string answer)
        {
            if (answer == _current.Word)
            {
                _current.CorrectCount += 1;
            } 
            else
            {
                _current.ErrorCount += 1;
            }
            return NextQuestion();
        }


    }

    public class xQuestion
    {
        public string Word { get; set; }
        public string ContextSentence { get; set; }
        public UInt16 CorrectCount { get; set; }
        public UInt16 ErrorCount { get; set; }
        public QuestionResult Result { get; set; }

    }

    public enum QuestionResult
    {
        CORRECT,
        WRONG,
        SKIPPED
    };

}
