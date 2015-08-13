using System;
using System.Windows.Input;

namespace MVVMDemo.Commands
{
    // This class allows delegating the commanding logic to methods passed as parameters,
    //  and enables a View to bind commands to objects that are not part of the element tree.
    public class CustomDelegateCommand : ICommand
    {
        private readonly Action<object> _executeMethod = null;
        private readonly Func<bool> _canExecuteMethod = null;
        private bool _isAutomaticRequeryDisabled = false;

        public CustomDelegateCommand(Action<object> executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("ExecuteMethod is not set.");
            }
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
            _isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            if (_executeMethod != null)
            {
                _executeMethod(null);
            }
        }
        
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
    }
}