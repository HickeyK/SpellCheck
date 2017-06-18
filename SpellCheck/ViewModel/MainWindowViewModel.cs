namespace SpellCheck.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private TestListViewModel _TestListViewModel = new TestListViewModel();

        public BindableBase _CurrentViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
        }

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }


        public RelayCommand<string> NavCommand { get; set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "TestList":
                    CurrentViewModel = _TestListViewModel;
                    break;
            }
        }


    }
}
