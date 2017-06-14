using SpellCheck.Entities;
using SpellCheck.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SpellCheck.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private ConnectedRepository _repo;
        //private SpellTestService _service;
        //private Action<SpellTest> ShowAnswerDialog;

        #region Construction


        public MainWindowViewModel()
        {

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;


            _repo = new ConnectedRepository();

            //_service = new SpellTestService();

            // Pass in the repo
            Tests = new ObservableCollection<SpellTest>(_repo.GetTests());
            _currentTest = new SpellTest();

            BeginCommand = new RelayCommand(OnBegin, CanBegin);

        }

        #endregion





        #region Properties


        public RelayCommand BeginCommand { get; set; }

        public ObservableCollection<SpellTest> Tests { get; set; }


        private SpellTest _currentTest;
        public SpellTest CurrentTest
        {
            get
            {
                return _currentTest;
            }

            set
            {
                if (_currentTest.Id == value.Id) return;

                _currentTest = value;


                Spellings = new ObservableCollection<SpellingViewModel>(

                    _repo.GetSpellings(_currentTest.Id).Select(
                        s => new SpellingViewModel(s)
                      )
                    );

            }
        }

        private ObservableCollection<SpellingViewModel> _spellings;
        public ObservableCollection<SpellingViewModel> Spellings
        {
            get
            {
                return _spellings;
            }
            set
            {
                _spellings = value;
                OnPropertyChanged("Spellings");
            }
        }


        #endregion


        #region INPC

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion



        #region EventHandling


        private void OnBegin()
        {
            AnswerDialogViewModel advm = new AnswerDialogViewModel(new List<SpellingViewModel>(Spellings),
                new SpeachService());
            var dialog = new View.AnswerDialog(advm);
            dialog.ShowDialog();
            OnPropertyChanged("Spellings");

        }

        private bool CanBegin()
        {
            return _currentTest != null;
        }


        #endregion

    }
}
