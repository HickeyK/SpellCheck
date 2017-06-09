using System.ComponentModel.DataAnnotations;

namespace SpellCheck.Entities
{
    public class Spelling 
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string ContextSentence { get; set; }

    }
}
