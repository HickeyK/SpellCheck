using System;
using SpellCheck.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SpellCheck.Services;

namespace SpellCheck.ViewModel
{
    public class TestListViewModel : BindableBase, IApplicationState
    {

        #region fields

        private IConnectedRepository _repo;


        #endregion

        #region Construction


        public TestListViewModel(IConnectedRepository repo)
        {

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;


            _repo = repo;


            Tests = new ObservableCollection<SpellTest>(_repo.GetTests());
            //CurrentTest = Tests.FirstOrDefault();
        }

        #endregion





        #region Properties


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
            get {return _spellings; }
            set
            {
                SetProperty(ref _spellings, value);
            }
        }


        #endregion



        #region EventHandling



        #endregion

        public Func<bool> CanBegin => TestSelected;
        public Func<string, bool> CanAdd { get; } = (s) => true;

        public Func<string, bool> CanEdit 
        {
            get { return (s) => TestSelected(); }
        }
        public Func<bool> CanShowResults => TestSelected;
        public Func<Window, bool> CanQuit { get; } = (w) => true;
        private bool TestSelected() => _currentTest != null;

        public IApplicationState OnBegin(ConnectedRepository repo)
        {
            AnswerViewModel avm = new AnswerViewModel(
                    this.CurrentTest.Id,
                    this.Spellings,
                    new SpeachService(),
                    repo);

            return avm;
        }
    }
}
