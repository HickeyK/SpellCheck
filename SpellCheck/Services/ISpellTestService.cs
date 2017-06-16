using SpellCheck.ViewModel;

namespace SpellCheck.Services
{
    public interface ISpellTestService
    {
        SpellingViewModel AnswerQuestion(string answer);
        SpellingViewModel NextQuestion();
        SpellingViewModel SkipQuestion();
    }
}