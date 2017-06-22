namespace SpellCheck.Entities
{
    public class TestAnswer
    {
        public int Id { get; set; }

        public int SpellingId { get; set; }

        public string FinalAnswer { get; set; }

        public int NumberOfTries { get; set; }

        public string AnswerStatus { get; set; }
    }
}
