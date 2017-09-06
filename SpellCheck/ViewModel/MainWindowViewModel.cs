using System;
using SpellCheck.Entities;
using SpellCheck.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SpellCheck.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        #region Fields

        private readonly ConnectedRepository _repo = new ConnectedRepository();

        private TestListViewModel _testListViewModel;
        private readonly AddEditTestViewModel _addEditTestViewModel;
        private readonly TestOccuranceViewModel _testOccuranceViewModel;


        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            _testListViewModel = new TestListViewModel(_repo);


            _addEditTestViewModel = new AddEditTestViewModel(_repo);
            _testOccuranceViewModel = new TestOccuranceViewModel(_repo);


            CanBegin = () => ((IApplicationState) CurrentViewModel).CanBegin();
            CanAdd = (s) => ((IApplicationState) CurrentViewModel).CanAdd(s);
            CanEdit = (s) => ((IApplicationState) CurrentViewModel).CanEdit(s);
            CanShowResults = () => ((IApplicationState)CurrentViewModel).CanShowResults();
            CanQuit = (w) => ((IApplicationState)CurrentViewModel).CanQuit(w);



            BeginCommand = new RelayCommand(OnBegin, CanBegin);
            AddCommand = new RelayCommand<string>(OnAddEdit, CanAdd);
            EditCommand = new RelayCommand<string>(OnAddEdit, CanEdit);
            ShowResultsCommand = new RelayCommand(OnShowResults, CanShowResults);
            QuitCommand = new RelayCommand<Window>(OnQuit, CanQuit);


            // For screen refresh
            _addEditTestViewModel.Done += () =>
            {
                CurrentViewModel = _testListViewModel;
            };

            _addEditTestViewModel.DeleteDone += () =>
            {
                _testListViewModel.CurrentTest = _testListViewModel.Tests.FirstOrDefault();
                CurrentViewModel = _testListViewModel;
            };

            _testOccuranceViewModel.Done += () =>
            {
                CurrentViewModel = _testListViewModel;
            }; 

            _addEditTestViewModel.SpellingAdded += _AddEditTestViewModel_SpellingAdded;
            _addEditTestViewModel.SpellingDeleted += _AddEditTestViewModel_SpellingDeleted;
            _testListViewModel.PropertyChanged += TestListViewModel_PropertyChanged;

            CurrentViewModel = _testListViewModel;

        }


        #endregion


        #region properties

        public RelayCommand BeginCommand { get; set; }
        public RelayCommand<string> AddCommand { get; set; }
        public RelayCommand<string> EditCommand { get; set; }
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
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                ShowResultsCommand.RaiseCanExecuteChanged();

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
                AddCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                ShowResultsCommand.RaiseCanExecuteChanged();
            }
        }

        private void _AddEditTestViewModel_SpellingAdded()
        {
            _testListViewModel.Tests.Add(_addEditTestViewModel.CurrentTest);
            _testListViewModel.CurrentTest = _addEditTestViewModel.CurrentTest;
        }

        private void _AddEditTestViewModel_SpellingDeleted()
        {
            _testListViewModel.Tests.Remove(_addEditTestViewModel.CurrentTest);
        }

        // Begin spelling test
        protected void OnBegin()
        {

            //AnswerViewModel avm = new AnswerViewModel(
            //    ((TestListViewModel)CurrentViewModel).CurrentTest.Id,
            //    ((TestListViewModel)CurrentViewModel).Spellings,
            //    new SpeachService(),
            //    _repo);

            CurrentViewModel = (BindableBase) ((IApplicationState) CurrentViewModel).OnBegin(_repo);

            ((IApplicationState) CurrentViewModel).Done += () =>
                CurrentViewModel = _testListViewModel;

            //CurrentViewModel = avm;

            OnPropertyChanged("Spellings");
        }



        private void OnAddEdit(string destination)
        {
            switch (destination)
            {

                case "AddTest":
                    _addEditTestViewModel.CurrentTest = new SpellTest();
                    _addEditTestViewModel.Spellings = new ObservableCollection<SpellingViewModel>();
                    _addEditTestViewModel.EditMode = false;
                    CurrentViewModel = _addEditTestViewModel;
                    break;

                case "EditTest":
                    _addEditTestViewModel.CurrentTest = _testListViewModel.CurrentTest;
                    _addEditTestViewModel.Spellings = _testListViewModel.Spellings;
                    _addEditTestViewModel.EditMode = true;
                    CurrentViewModel = _addEditTestViewModel;
                    break;

            }
        }

        // Wired up in constructor
        private void NavToTestList() => CurrentViewModel = _testListViewModel;



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


        public Func<bool> CanBegin { get; }
        public Func<string, bool> CanAdd { get; }
        public Func<string, bool> CanEdit { get; }
        public Func<bool> CanShowResults { get; }
        public Func<Window, bool> CanQuit { get; }
    }
}
