using System;
using System.Windows.Input;

namespace ContactMgmt
{
    /// <summary>
    /// An ICommand whose delegates can be attached for Execute(T) and CanExecute(T). It also implements the IActiveAware interface, which is useful when registering this command in a CompositeCommand that monitors command's activity.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;
        
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///  Constructor takes action and predicate
        /// </summary>
        /// <param name="action"></param>
        /// <param name="canExecute"></param>
        public DelegateCommand(Action<object> action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter"></param>
        public  void Execute(object parameter)
        {
            _action(parameter);
        }

        /// <summary>
        /// Raises CanExecuteChanged on the UI thread so every command invoker can requery to check if the command can execute.
        /// Note that this will trigger the execution of CanExecute(T) once for each invoker.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}