using SpellCheck.Entities;
using SpellCheck.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace SpellCheck.ViewModel
{
    public class AnswerDialogViewModel : INotifyPropertyChanged
    {


        private SpellTestService _spellTestService; 

        public AnswerDialogViewModel(List<SpellingViewModel> _spellTests)
        {
        

            _spellTestService = new SpellTestService(_spellTests);

            CurrentSpelling = _spellTestService.NextQuestion();


            AnswerCommand = new RelayCommand(OnAnswer, CanAnswer);
            //SkipCommand = new RelayCommand(OnSkip, CanSkip);
            QuitCommand = new RelayCommand<Window>(OnQuit);
            //RepeatCommand = new RelayCommand(OnRepeat, CanRepeat);

        }

        //public SpellTest SpellTest { get; set; }


        public RelayCommand AnswerCommand { get; set; }
        public RelayCommand SkipCommand { get; set; }
        public RelayCommand<Window> QuitCommand { get; set; }
        public RelayCommand RepeatCommand { get; set; }



        private SpellingViewModel _currentSpelling;
        public SpellingViewModel CurrentSpelling
        {
            get { return _currentSpelling; }
            set
            {
                if (value != _currentSpelling)
                {
                    _currentSpelling = value;
                    OnPropertyChanged("CurrentSpelling");
                }
            }
        }

        private string _currentAnswer;
        public string CurrentAnswer
        {
            get { return _currentAnswer; }
            set
            {
                if (value != _currentAnswer)
                {
                    _currentAnswer = value;
                    OnPropertyChanged("CurrentAnswer");

                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }



        private void OnAnswer()
        {
            var next = _spellTestService.AnswerQuestion(CurrentAnswer);
            CurrentSpelling = next;
            CurrentAnswer = "";

            if (next == null)
            {
                // All questions answered
                // Appears that setting the property to null was not triggering an evaluation of CanExecuteChanged post command execution.
                // It was being called just before command invocation but the command is already triggered at that point so what's the point.
                AnswerCommand.RaiseCanExecuteChanged();

            }
        }

        private bool CanAnswer()
        {
            return CurrentSpelling != null;
        }

        private void OnQuit(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }


    }
}
