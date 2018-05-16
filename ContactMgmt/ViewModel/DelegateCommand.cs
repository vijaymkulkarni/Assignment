using System;
using System.Windows.Input;

namespace ContactMgmt
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;
        private Action<object> save_Command;
        
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public  bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public  void Execute(object parameter)
        {
            _action(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}