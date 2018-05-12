using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ContactMgmt
{
    public partial class DelegateCommand : ICommand
    {
        private readonly Action<Object> _action;
        
        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
#pragma warning restore 67
    }
}
