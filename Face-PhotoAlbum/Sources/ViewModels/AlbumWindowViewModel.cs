using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class AlbumWindow : ObservableObject, IAlbumWindowViewModel {
        private ObservableCollection<FaceAlbumViewModel> _FaceAlbums;
        private ICommand _AddFaceAlbumCommand;
        public ObservableCollection<FaceAlbumViewModel> FaceAlbums { get; set; }
        public ICommand AddFaceAlbum
        {
            get
            {
                if (this._AddFaceAlbumCommand == null)
                {
                    //this._AddFaceAlbum = new CommandProxy(WaitingAction);
                }
                return this._AddFaceAlbumCommand;
            }
        }
    }
}