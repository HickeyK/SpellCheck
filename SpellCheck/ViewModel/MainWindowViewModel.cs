using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpellCheck.Entities;
using System.ComponentModel;

namespace SpellCheck.ViewModel 
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private ConnectedRepository _repo = new ConnectedRepository();

        public MainWindowViewModel ()
        {
            // Pass in the repo
            Tests = _repo.Tests;

        }

        public List<SpellTest> Tests { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate{};
    }
}
