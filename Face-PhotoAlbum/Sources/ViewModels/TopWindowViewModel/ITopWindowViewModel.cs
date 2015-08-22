using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    interface ITopWindowViewModel {
        ICommand Waiting { get; }
    }
}
