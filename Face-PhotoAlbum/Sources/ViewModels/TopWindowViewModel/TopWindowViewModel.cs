using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Face_PhotoAlbum.ViewModels {
    public class TopWindowViewModel : BindableBase, ITopWindowViewModel, INotification {
        public TopWindowViewModel():base() {
            Title = "TopWindow";
        }
        public object Content { get; set; }
        public string Title { get; set; }

        private ICommand _WaitingCommand;

        public event EventHandler HasWaited;

        protected virtual void OnWaited() {
            var handler = this.HasWaited;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        public ICommand WaitingCommand {
            get {
                if (this._WaitingCommand == null) {
                    this._WaitingCommand = new CommandProxy(WaitingAction);
                }
                return this._WaitingCommand;
            }
        }

        private void WaitingAction(object parameter) {
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (o, a) => {
                timer.Stop();
                OnWaited();

            };

            timer.Start();
        }
    }
}
