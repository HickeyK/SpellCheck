using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpellCheck.Entities
{
    public class Spelling 
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string ContextSentence { get; set; }

        [NotMapped]
        public UInt16 CorrectCount { get; set; }
        [NotMapped]
        public UInt16 Skipped { get; set; }
        [NotMapped]
        public UInt16 ErrorCount { get; set; }
        [NotMapped]
        public QuestionResult Result { get; set; }

    }

    public enum QuestionResult
    {
        CORRECT,
        WRONG,
        SKIPPED
    };
}
