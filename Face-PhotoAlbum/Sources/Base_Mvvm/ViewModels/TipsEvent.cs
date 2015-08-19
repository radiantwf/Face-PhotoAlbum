using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.ViewModels {
    public class TipsEventArgs : EventArgs {
        public string Tips { get; private set; }

        public TipsEventArgs(string tips)
        {
            this.Tips = tips;
        }
    }

    public delegate void TipsEventHandler(object sender, TipsEventArgs e);
}
