using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.Views.Commands {
    public abstract class BaseCommandBindingProxy {
        public static implicit operator CommandBinding(BaseCommandBindingProxy proxy)
        {
            return proxy.CommandBinding;
        }

        public abstract CommandBinding CommandBinding { get; }
    }
}
