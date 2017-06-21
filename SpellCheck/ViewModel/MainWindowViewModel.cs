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

        private ConnectedRepository _repo = new ConnectedRepository();
        private TestListViewModel _TestListViewModel;
        private AddEditTestViewModel _AddEditTestViewModel;

        #endregion

        #region constructors

        public MainWindowViewModel()
        {
            BeginCommand = new RelayCommand(OnBegin, CanBegin);
            NavCommand = new RelayCommand<string>(OnNav);
            EditCommand = new RelayCommand(OnEdit, CanEdit);
            QuitCommand = new RelayCommand<Window>(OnQuit);

            _TestListViewModel = new TestListViewModel(_repo);
            _AddEditTestViewModel = new AddEditTestViewModel(_repo);

            _AddEditTestViewModel.Done += NavToTestList;
            _AddEditTestViewModel.SpellingAdded += _AddEditTestViewModel_SpellingAdded;
            _TestListViewModel.PropertyChanged += TestListViewModel_PropertyChanged;
            CurrentViewModel = _TestListViewModel;
        }


        #endregion


        #region properties

        public RelayCommand BeginCommand { get; set; }
        public RelayCommand<string> NavCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand<Window> QuitCommand { get; set; }


        public BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set
            {
                SetProperty(ref _CurrentViewModel, value);
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
            _TestListViewModel.Tests.Add(_AddEditTestViewModel.CurrentTest);
        }


        protected void OnBegin()
        {

            AnswerDialogViewModel advm = new AnswerDialogViewModel(((TestListViewModel)CurrentViewModel).Spellings,
                new SpeachService());

            var dialog = new View.AnswerDialog(advm);
            dialog.ShowDialog();
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
                    CurrentViewModel = _TestListViewModel;
                    break;

                case "AddTest":
                    _AddEditTestViewModel.CurrentTest = new SpellTest();
                    _AddEditTestViewModel.Spellings = new ObservableCollection<SpellingViewModel>();
                    _AddEditTestViewModel.EditMode = false;
                    CurrentViewModel = _AddEditTestViewModel;
                    break;

            }
        }


        protected void OnEdit()
        {
            _AddEditTestViewModel.CurrentTest = _TestListViewModel.CurrentTest;
            _AddEditTestViewModel.Spellings = _TestListViewModel.Spellings; ;
            _AddEditTestViewModel.EditMode = true;
            CurrentViewModel = _AddEditTestViewModel;
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
            CurrentViewModel = _TestListViewModel;
        }


        protected void OnQuit(Window window)
        {
            if (window != null) window.Close();
        }

        #endregion
    }
}
