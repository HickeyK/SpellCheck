using SpellCheck.Entities;
using SpellCheck.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellCheck.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private ConnectedRepository _repo;
        private SpellService _service;

        public MainWindowViewModel ()
        {

            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;


            _repo = new ConnectedRepository();

            _service = new SpellService();

            // Pass in the repo
            Tests = new ObservableCollection<SpellTest>(_repo.GetTests());
            _currentTest = new SpellTest();

            BeginCommand = new RelayCommand(OnBegin, CanBegin);

        }





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
                Spellings = new ObservableCollection<Spelling>(
                    _repo.GetSpellings(_currentTest.Id));

            }
        }

        private ObservableCollection<Spelling> _spellings;
        public ObservableCollection<Spelling> Spellings
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


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate{};

        private void OnBegin()
        {
            foreach (var spelling in Spellings)
            {


            }

        }

        private bool CanBegin()
        {
            return _currentTest != null;
        }
    }
}
