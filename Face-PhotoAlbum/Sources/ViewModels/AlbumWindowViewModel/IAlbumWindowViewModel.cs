using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    interface IAlbumWindowViewModel {
        ObservableCollection<FaceAlbumViewModel> FaceAlbums { get; set; }
        ICommand ReadFaceAlbumsCommand { get; }
        ICommand SelectFaceAlbumCommand { get; }
        ICommand EnterFaceAlbumCommand { get; }
    }
}
