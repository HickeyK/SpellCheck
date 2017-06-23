using SpellCheck.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellCheck.ViewModel
{
    class TestOccuranceViewModel : BindableBase
    {
        #region Fields

        private IConnectedRepository _repo;

        #endregion


        #region Constructors

        public TestOccuranceViewModel(IConnectedRepository repo)
        {
            if (DesignerProperties.GetIsInDesignMode(
                 new System.Windows.DependencyObject())) return;


            _repo = repo;

        }

        #endregion


        #region Events
        #endregion


        #region Properties

        private SpellTest _spellTest;

    public SpellTest SpellTest
        {
            get { return _spellTest; }
            set
            {
                SetProperty(ref _spellTest, value);
                TestOccurances = new ObservableCollection<TestOccurance>(_repo.GetTestOccurances(_spellTest.Id));
            }
        }

        private ObservableCollection<TestOccurance> _testOccurances;

        public ObservableCollection<TestOccurance> TestOccurances
        {
            get { return _testOccurances; }
            set
            {
                SetProperty(ref _testOccurances, value);
            }
        }





        private TestOccurance _currentTestOccurance;
        public TestOccurance CurrentTestOccurance
        {
            get
            {
                return _currentTestOccurance;
            }
            set
            {
                if (_currentTestOccurance != null && _currentTestOccurance.Id == value.Id) return;

                SetProperty(ref _currentTestOccurance, value);

                Answers = new ObservableCollection<TestAnswer>(
                    _repo.GetAnswers(CurrentTestOccurance.Id));

                    //_repo.GetAnswers(_currentTestOccurance.Id).Select(
                    //    s => new SpellingViewModel(s)
                    //  )
                    //);
            }
        }

        private ObservableCollection<TestAnswer> _answers;
        public ObservableCollection<TestAnswer> Answers
        {
            get { return _answers; }
            set
            {
                SetProperty(ref _answers, value);
            }
        }







        #endregion


        #region Methods
        #endregion


        #region EventHandlers
        #endregion

    }
}
