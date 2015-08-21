using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class AlbumWindow : ObservableObject/*, IAlbumWindowViewModel*/ {
        private ObservableCollection<FaceAlbumViewModel> _FaceAlbums;

        private ICommand _SelectFaceAlbumCommand;
        private ICommand _EnterFaceAlbumCommand;

        public ObservableCollection<FaceAlbumViewModel> FaceAlbums { get; set; }
        public ICommand SelectFaceAlbumCommand
        {
            get
            {
                if (this._SelectFaceAlbumCommand == null)
                {
                    this._SelectFaceAlbumCommand = new CommandProxy(SelectFaceAlbum);
                }
                return this._SelectFaceAlbumCommand;
            }
        }
        public ICommand EnterFaceAlbumCommand
        {
            get
            {
                if (this._EnterFaceAlbumCommand == null)
                {
                    this._EnterFaceAlbumCommand = new CommandProxy(EnterFaceAlbum);
                }
                return this._EnterFaceAlbumCommand;
            }
        }

        private void SelectFaceAlbum(object parameter)
        {
        }
        private void EnterFaceAlbum(object parameter)
        {
        }

    }
}