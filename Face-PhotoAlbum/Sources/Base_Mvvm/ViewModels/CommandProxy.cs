using System;
using System.Windows.Input;

namespace Face_PhotoAlbum.Base.ViewModel {
    /// <summary>
    /// 命令代理
    /// </summary>
    public class CommandProxy : ICommand {
        #region Fields

        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        #endregion

        #region Constructors

        public CommandProxy(Action<object> execute)
            : this(execute, null)
        {
        }

        public CommandProxy(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion
    }
}
