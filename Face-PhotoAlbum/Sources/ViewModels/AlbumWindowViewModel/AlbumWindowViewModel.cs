using Face_PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class AlbumWindowViewModel : ObservableObject, IAlbumWindowViewModel {
        private ObservableCollection<FaceAlbumViewModel> _FaceAlbums = null;

        private AlbumWindowModel model = new AlbumWindowModel();
        private ICommand _ReadFaceAlbumsCommand;
        private ICommand _SelectFaceAlbumCommand;
        private ICommand _EnterFaceAlbumCommand;

        public ObservableCollection<FaceAlbumViewModel> FaceAlbums {
            get {
                return _FaceAlbums;
            }
            set {
                _FaceAlbums = value;
                RaisePropertyChanged(() => FaceAlbums);
            }
        }

        public ICommand ReadFaceAlbumsCommand {
            get {
                if (this._ReadFaceAlbumsCommand == null) {
                    this._ReadFaceAlbumsCommand = new CommandProxy(ReadFaceAlbums);
                }
                return this._ReadFaceAlbumsCommand;
            }
        }
        public ICommand SelectFaceAlbumCommand {
            get {
                if (this._SelectFaceAlbumCommand == null) {
                    this._SelectFaceAlbumCommand = new CommandProxy(SelectFaceAlbum);
                }
                return this._SelectFaceAlbumCommand;
            }
        }
        public ICommand EnterFaceAlbumCommand {
            get {
                if (this._EnterFaceAlbumCommand == null) {
                    this._EnterFaceAlbumCommand = new CommandProxy(EnterFaceAlbum);
                }
                return this._EnterFaceAlbumCommand;
            }
        }

        private void ReadFaceAlbums(object parameter) {
            try {
                var modelData = model.GetFaceAlbumList();
                FaceAlbums = FaceAlbumViewModel.ConvertToViewModelDataList(modelData);
            }
            catch(Exception ex) {
                throw;
            }
        }
        private void SelectFaceAlbum(object parameter) {
            try {
            }
            catch {
                throw;
            }
        }
        private void EnterFaceAlbum(object parameter) {
            try {
            }
            catch {
                throw;
            }
        }
    }
}