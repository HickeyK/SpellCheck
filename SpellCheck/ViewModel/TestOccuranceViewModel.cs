using SpellCheck.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellCheck.ViewModel
{
    class TestOccuranceViewModel : BindableBase
    {
        #region Fields

        private IConnectedRepository _repo;

        #endregion


        #region Constructors

        public TestOccuranceViewModel(IConnectedRepository repo)
        {
            if (DesignerProperties.GetIsInDesignMode(
                 new System.Windows.DependencyObject())) return;


            _repo = repo;


            //TestOccurances = new ObservableCollection<TestOccurance>(_repo.GetTests());
        }

        #endregion


        #region Events
        #endregion


        #region Properties

        private ObservableCollection<TestOccurance> _testOccurances;

        public ObservableCollection<TestOccurance> TestOccurances
        {
            get { return _testOccurances; }
            set
            {
                SetProperty(ref _testOccurances, value);
            }
        }
        #endregion


        #region Methods
        #endregion


        #region EventHandlers
        #endregion

    }
}
