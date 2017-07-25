using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpellCheck.Entities
{
    public class Spelling 
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string ContextSentence { get; set; }
    }

    public enum QuestionResult
    {
        CORRECT,
        WRONG,
        SKIPPED
    };
}
