using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Face_PhotoAlbum.Views.Commands {
    public class CloseCommandBindingProxy : BaseCommandBindingProxy {
        private Window window;
        private CommandBinding _CommandBinding;
        public override CommandBinding CommandBinding
        {
            get { return this._CommandBinding; }
        }

        public CloseCommandBindingProxy(Window window)
        {
            this.window = window;

            _CommandBinding = new CommandBinding(ApplicationCommands.Close,
                CommandExecuted,
                CommandCanExecute);
        }

        void CommandExecuted(object target, ExecutedRoutedEventArgs e)
        {
            this.window.Close();
        }

        void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
