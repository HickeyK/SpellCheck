using SpellCheck.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpellCheck.Services
{
    public class SpellTestService : ISpellTestService
    {
        private SpellingViewModel _current;
        private readonly ISpeachService _speachService;
        private readonly Random _rnd = new Random();

        private ObservableCollection<SpellingViewModel> Questions { get; set; }


        public SpellTestService(ObservableCollection<SpellingViewModel> questions, ISpeachService speachService)
        {
            Questions = questions;
            _speachService = speachService;
        }

        public SpellingViewModel NextQuestion()
        {
            var unanswered = Questions.Where(q => q.CorrectCount < 1 && !q.Skipped);
            if (unanswered.Count() == 0)
            {
                return null;
            }
            int i = _rnd.Next(0, unanswered.Count());
            _current = unanswered.ElementAt(i);
            return _current;
        }

        public SpellingViewModel AnswerQuestion(string answer)
        {
            _current.FinalAnswer = answer;
            if (answer.ToUpper() == _current.Word.ToUpper())
            {
                _current.CorrectCount += 1;
                _current.AnswerStatus  = Entities.QuestionResult.Correct;
                _speachService.Say("Correct.", false);
            } 
            else
            {
                _current.ErrorCount += 1;
                _current.AnswerStatus = Entities.QuestionResult.Wrong;
                _speachService.Say("Wrong.", false);
            }
            return NextQuestion();
        }

        public SpellingViewModel SkipQuestion()
        {
            _current.Skipped = true;
            _current.AnswerStatus = Entities.QuestionResult.Skipped;
            return NextQuestion();
        }


    }

}
