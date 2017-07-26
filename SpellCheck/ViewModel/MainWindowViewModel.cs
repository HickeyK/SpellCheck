using SpellCheck.Entities;
using SpellCheck.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SpellCheck.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly ConnectedRepository _repo = new ConnectedRepository();
        private readonly TestListViewModel _testListViewModel;
        private readonly AddEditTestViewModel _addEditTestViewModel;
        //private AnswerViewModel _AnswerViewModel;
        private readonly TestOccuranceViewModel _testOccuranceViewModel;

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            BeginCommand = new RelayCommand(OnBegin, CanBegin);
            NavCommand = new RelayCommand<string>(OnNav);
            EditCommand = new RelayCommand(OnEdit, CanEdit);
            ShowResultsCommand = new RelayCommand(OnShowResults);
            QuitCommand = new RelayCommand<Window>(OnQuit);

            _testListViewModel = new TestListViewModel(_repo);
            _addEditTestViewModel = new AddEditTestViewModel(_repo);
            //_AnswerViewModel = new AnswerViewModel();
            _testOccuranceViewModel = new TestOccuranceViewModel(_repo);


            _addEditTestViewModel.Done += NavToTestList;
            _addEditTestViewModel.SpellingAdded += _AddEditTestViewModel_SpellingAdded;
            _testListViewModel.PropertyChanged += TestListViewModel_PropertyChanged;
            CurrentViewModel = _testListViewModel;
        }


        #endregion


        #region properties

        public RelayCommand BeginCommand { get; set; }
        public RelayCommand<string> NavCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand ShowResultsCommand { get; set; }
        public RelayCommand<Window> QuitCommand { get; set; }


        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                SetProperty(ref _currentViewModel, value);
                BeginCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }


        #endregion


        #region methods



        #endregion

        #region event handlers

        private void TestListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentTest")
            {
                BeginCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private void _AddEditTestViewModel_SpellingAdded()
        {
            _testListViewModel.Tests.Add(_addEditTestViewModel.CurrentTest);
        }


        // Begin spelling test
        protected void OnBegin()
        {

            AnswerViewModel avm = new AnswerViewModel(
                ((TestListViewModel)CurrentViewModel).CurrentTest.Id,
                ((TestListViewModel)CurrentViewModel).Spellings,
                new SpeachService(),
                _repo);

            avm.Done += NavToTestList;

            CurrentViewModel = avm;
            OnPropertyChanged("Spellings");
        }

        protected bool CanBegin()
        {

            if (CurrentViewModel.GetType() == typeof(TestListViewModel))
            {
                if (((TestListViewModel)CurrentViewModel).CurrentTest != null)
                {
                    return true;
                }
            }
            return false;
        }


        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "TestList":
                    CurrentViewModel = _testListViewModel;
                    break;

                case "AddTest":
                    _addEditTestViewModel.CurrentTest = new SpellTest();
                    _addEditTestViewModel.Spellings = new ObservableCollection<SpellingViewModel>();
                    _addEditTestViewModel.EditMode = false;
                    CurrentViewModel = _addEditTestViewModel;
                    break;

            }
        }


        protected void OnEdit()
        {
            _addEditTestViewModel.CurrentTest = _testListViewModel.CurrentTest;
            _addEditTestViewModel.Spellings = _testListViewModel.Spellings; 
            _addEditTestViewModel.EditMode = true;
            CurrentViewModel = _addEditTestViewModel;
        }

        protected bool CanEdit()
        {

            if (CurrentViewModel.GetType() == typeof(TestListViewModel))
            {
                if (((TestListViewModel)CurrentViewModel).CurrentTest != null)
                {
                    return true;
                }
            }
            return false;
        }



        private void NavToTestList()
        {
            CurrentViewModel = _testListViewModel;
        }

        private void OnShowResults()
        {
            _testOccuranceViewModel.CurrentTest = _testListViewModel.CurrentTest;
            CurrentViewModel = _testOccuranceViewModel;
        }


        protected void OnQuit(Window window)
        {
            window?.Close();
        }


        #endregion
    }
}
