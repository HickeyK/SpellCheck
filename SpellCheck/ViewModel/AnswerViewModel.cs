using SpellCheck.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using SpellCheck.Entities;

namespace SpellCheck.ViewModel
{
    public class AnswerViewModel : BindableBase, IApplicationState
    {

        #region Fields

        private readonly ISpellTestService _spellTestService;
        private readonly ISpeachService _speachService;
        private ConnectedRepository _repo;
        private ObservableCollection<SpellingViewModel> _spellTests;
        private readonly int _spellTestId;


        #endregion

        #region Constructors

        #endregion

        public AnswerViewModel(int spellTestId,
                               ObservableCollection<SpellingViewModel> spellTests,
                               ISpeachService speachService,
                               ConnectedRepository repo)
        {
            _spellTestId = spellTestId;
            _spellTestService = new SpellTestService(spellTests, speachService);
            _speachService = speachService;
            _repo = repo;
            _spellTests = spellTests;

            CurrentSpelling = _spellTestService.NextQuestion();

            RequestSpelling(CurrentSpelling);

            AnswerCommand = new RelayCommand<Window>(OnAnswer, CanAnswer);
            SkipCommand = new RelayCommand<Window>(OnSkip, CanSkip);
            QuitCommand = new RelayCommand<Window>(OnQuit);
            RepeatCommand = new RelayCommand(OnRepeat, CanRepeat);

        }

        #region Events

        public event Action Done = delegate { };

        #endregion

        #region Properties

        public RelayCommand<Window> AnswerCommand { get; set; }
        public RelayCommand<Window> SkipCommand { get; set; }
        public RelayCommand<Window> QuitCommand { get; set; }
        public RelayCommand RepeatCommand { get; set; }


        private SpellingViewModel _currentSpelling;

        public SpellingViewModel CurrentSpelling
        {
            get { return _currentSpelling; }
            set { SetProperty(ref _currentSpelling, value); }
        }


        private string _currentAnswer;

        public string CurrentAnswer
        {
            get { return _currentAnswer; }
            set { SetProperty(ref _currentAnswer, value); }
        }


        #endregion


        #region EventHandlers


        private void OnAnswer(Window window)
        {
            var next = _spellTestService.AnswerQuestion(CurrentAnswer);
            CurrentSpelling = next;
            CurrentAnswer = "";

            if (next == null)
            {
                // All questions answered
                _speachService.Say("Test Completed", true);
                SaveAnswers();
                OnQuit(window);
                return;
            }

            RequestSpelling(CurrentSpelling);

        }


        private bool CanAnswer(Window window)
        {
            return CurrentSpelling != null;
        }

        private void OnSkip(Window window)
        {
            var next = _spellTestService.SkipQuestion();
            CurrentSpelling = next;
            CurrentAnswer = "";

            if (next == null)
            {
                // All questions answered
                _speachService.Say("Test Completed", true);
                SaveAnswers();
                OnQuit(window);

            }

        }

        private void SaveAnswers()
        {
            var testOccurance = new TestOccurance
            {
                DateTestTaken = DateTime.Now,
                SpellTestId = _spellTestId
            };
            _repo.SaveTestOccurance(testOccurance);

            var testAnswers = new List<TestAnswer>();
            foreach (var st in _spellTests)
            {
                testAnswers.Add(
                    new TestAnswer
                    {
                        SpellTestId = _spellTestId,
                        TestOccuranceId = testOccurance.Id,
                        SpellingId = st.Id,
                        FinalAnswer = st.FinalAnswer,
                        NumberOfTries = st.CorrectCount + st.ErrorCount,
                        AnswerStatus = st.AnswerStatus.ToString()
                    });
            }
            _repo.SaveTestAnswers(testAnswers);
        }


        private bool CanSkip(Window window)
        {
            return CurrentSpelling != null;
        }



        private void OnQuit(Window window)
        {
            Done();
        }


        private void OnRepeat()
        {
            RequestSpelling(CurrentSpelling);
        }

        private bool CanRepeat()
        {
            return CurrentSpelling != null;
        }


        private void RequestSpelling(SpellingViewModel spellingViewModel)
        {
            var request = spellingViewModel.Word + "," +
                "As in," +
                spellingViewModel.ContextSentence;

            _speachService.Say(request, true);

        }

        #endregion


        public Func<bool> CanBegin { get; } = () => false;
        public Func<string, bool> CanAdd { get; } = (s) => false;
        public Func<string, bool> CanEdit { get; } = (s) => false;
        public Func<bool> CanShowResults { get; } = () => false;
        public Func<Window, bool> CanQuit { get; } = (w) => true;
    }
}
