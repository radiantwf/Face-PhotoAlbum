using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.ViewModels {
    public class FaceAlbumViewModel : ObservableObject {
        public int FaceAlbumName { get; set; }
        public byte[] FaceAlbumImage { get; set; }
        public byte[] ImageCount { get; set; }

        private bool _IsSelected = false;

        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }
    }
}
