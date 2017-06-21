using SpellCheck.Entities;
using SpellCheck.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SpellCheck.ViewModel
{
    public class TestListViewModel : BindableBase
    {

        private ConnectedRepository _repo;
        //private SpellTestService _service;
        //private Action<SpellTest> ShowAnswerDialog;

        #region Construction


        public TestListViewModel(ConnectedRepository repo)
        {

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;


            _repo = repo;


            Tests = new ObservableCollection<SpellTest>(_repo.GetTests());
            //_currentTest = Tests.FirstOrDefault();

            //BeginCommand = new RelayCommand(OnBegin, CanBegin);

        }

        #endregion





        #region Properties

        //public RelayCommand BeginCommand { get; set; }


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
                if (_currentTest != null && _currentTest.Id == value.Id) return;

                SetProperty(ref _currentTest, value);
                //BeginCommand.RaiseCanExecuteChanged();
                //_currentTest = value;

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



        #region EventHandling


        //private void OnBegin()
        //{
        //    AnswerDialogViewModel advm = new AnswerDialogViewModel(new ObservableCollection<SpellingViewModel>(Spellings),
        //        new SpeachService());
        //    var dialog = new View.AnswerDialog(advm);
        //    dialog.ShowDialog();
        //    OnPropertyChanged("Spellings");

        //}

        //private bool CanBegin()
        //{
        //    return _currentTest != null;
        //}


        #endregion

    }
}
