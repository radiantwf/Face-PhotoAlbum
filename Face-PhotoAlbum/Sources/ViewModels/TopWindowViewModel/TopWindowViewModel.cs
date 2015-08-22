using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Face_PhotoAlbum.ViewModels {
    public class TopWindowViewModel : ObservableObject, ITopWindowViewModel {
        private ICommand _LoginCommand;

        public event EventHandler HasWaited;

        protected virtual void OnWaited()
        {
            var handler = this.HasWaited;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        public ICommand Waiting
        {
            get
            {
                if (this._LoginCommand == null)
                {
                    this._LoginCommand = new CommandProxy(WaitingAction);
                }
                return this._LoginCommand;
            }
        }

        private void WaitingAction(object parameter)
        {
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(0);
            timer.Tick += (o, a) =>
            {
                timer.Stop();
                OnWaited();

            };

            timer.Start();
        }
    }
}
