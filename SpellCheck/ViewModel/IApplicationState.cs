using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpellCheck.ViewModel
{
    public interface IApplicationState
    {
        IApplicationState OnBegin(ConnectedRepository repo);

        Func<bool> CanBegin { get; }

        Func<string, bool> CanAdd { get; }

        Func<string, bool> CanEdit { get; }

        Func<bool> CanShowResults { get; }

        Func<Window, bool> CanQuit { get; }

        
    }
}
