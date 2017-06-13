using SpellCheck.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellCheck.Services
{
    class SpellTestService
    {

        Random rnd = new Random();

        public List<Spelling> Questions { get; set; }

        private Spelling _current;


        public SpellTestService(SpellTest _spellTest)
        {
            Questions = new List<Spelling>();

            Questions = _spellTest.Spellings;

            //foreach (var spelling in _spellTest.Spellings)
            //{
            //    Questions.Add(new Question {
            //        Word = spelling.Word,
            //        ContextSentence = spelling.ContextSentence
            //    } );
            //}

        }

        public Spelling NextQuestion()
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

        public Spelling AnswerQuestion(string answer)
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
