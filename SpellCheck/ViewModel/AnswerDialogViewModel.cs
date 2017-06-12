using SpellCheck.Entities;

namespace SpellCheck.ViewModel
{
    public class AnswerDialogViewModel
    {

        public SpellTest SpellTest { get; set; }

        public AnswerDialogViewModel(SpellTest _spellText)
        {
            SpellTest = _spellText;

        }
    }
}
