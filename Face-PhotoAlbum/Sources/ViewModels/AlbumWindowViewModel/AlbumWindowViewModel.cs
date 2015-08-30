using Face_PhotoAlbum.Models;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class AlbumWindowViewModel : BindableBase, IAlbumWindowViewModel {
        private ObservableCollection<AlbumContainerViewModel> _FaceAlbums = null;
        private ObservableCollection<PhotoContainerViewModel> _Photos = null;
        private int _CurrentAlbumNum = -1;
        private string _CurrentAlbumName = string.Empty;
        private InteractionRequest<INotification> _ShowTopWindowRequest = new InteractionRequest<INotification>();

        public enum ContentType { FaceAlbum, Photo }
        public ContentType _CurrentContentType = ContentType.FaceAlbum;

        public event EventHandler ShowPhotoDetailWindowEventHandler;
        protected virtual void OnShowPhotoDetailWindow() {
            var handler = this.ShowPhotoDetailWindowEventHandler;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        private AlbumWindowModel model = new AlbumWindowModel();
        private ICommand _ReadFaceAlbumsCommand;
        private ICommand _BackToAlbumsCommand;
        private ICommand _SearchPhotosCommand;

        public IInteractionRequest ShowTopWindowRequest {
            get { return this._ShowTopWindowRequest; }
        }
        public void ShowTopWindow() {
            this._ShowTopWindowRequest.Raise(new TopWindowViewModel());
        }

        public ContentType CurrentContentType {
            get {
                return _CurrentContentType;
            }
            set {
                _CurrentContentType = value;
                if (_CurrentContentType == ContentType.FaceAlbum) {
                    _CurrentAlbumNum = -1;
                    _CurrentAlbumName = string.Empty;
                }
                this.OnPropertyChanged(() => this.CurrentContentType);
                this.OnPropertyChanged(() => this.AlbumLabelContext);
            }
        }
        public string AlbumLabelContext {
            get {
                return _CurrentContentType == ContentType.FaceAlbum ? "相册封面" : "相册（" + _CurrentAlbumName + "）";
            }
        }
        public ObservableCollection<AlbumContainerViewModel> FaceAlbums {
            get {
                return _FaceAlbums;
            }
            set {
                _FaceAlbums = value;
                this.OnPropertyChanged(() => this.FaceAlbums);
            }
        }
        public ObservableCollection<PhotoContainerViewModel> Photos {
            get {
                return _Photos;
            }
            set {
                _Photos = value;
                this.OnPropertyChanged(() => this.Photos);
                this.OnPropertyChanged(() => this.ComfirmMatchPhotos);
                this.OnPropertyChanged(() => this.PossibleMatchPhotos);
                this.OnPropertyChanged(() => this.UnknownPhotos);
                this.OnPropertyChanged(() => this.ComfirmMatchPhotosShownFlg);
                this.OnPropertyChanged(() => this.PossibleMatchShownFlg);
                this.OnPropertyChanged(() => this.UnknownShownFlg);
            }
        }
        public List<PhotoContainerViewModel> ComfirmMatchPhotos {
            get {
                var list = _Photos == null ? null : _Photos.Where(p => p.MatchType == PhotoContainerModel.MatchTypeType.Confirm).ToList();
                return list;
            }
        }
        public List<PhotoContainerViewModel> PossibleMatchPhotos {
            get {
                var list = _Photos == null ? null : _Photos.Where(p => p.MatchType == PhotoContainerModel.MatchTypeType.Possible).ToList();
                return list;
            }
        }
        public List<PhotoContainerViewModel> UnknownPhotos {
            get {
                var list = _Photos == null ? null : _Photos.Where(p => p.MatchType == PhotoContainerModel.MatchTypeType.Unknown).ToList();
                return list;
            }
        }

        public bool ComfirmMatchPhotosShownFlg {
            get {
                return ComfirmMatchPhotos == null || ComfirmMatchPhotos.Count() == 0 ? false : true;
            }
        }
        public bool PossibleMatchShownFlg {
            get {
                return PossibleMatchPhotos == null || PossibleMatchPhotos.Count() == 0 ? false : true;
            }
        }
        public bool UnknownShownFlg {
            get {
                return UnknownPhotos != null && UnknownPhotos.Count() == _Photos.Count() ? true : false;
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
        public ICommand BackToAlbumsCommand {
            get {
                if (this._BackToAlbumsCommand == null) {
                    this._BackToAlbumsCommand = new CommandProxy(BackToAlbums);
                }
                return this._BackToAlbumsCommand;
            }
        }
        public ICommand SearchPhotosCommand {
            get {
                if (this._SearchPhotosCommand == null) {
                    this._SearchPhotosCommand = new CommandProxy(SearchPhotos);
                }
                return this._SearchPhotosCommand;
            }
        }
        private void ReadFaceAlbums(object parameter) {
            try {
                var modelData = model.GetFaceAlbumList();
                FaceAlbums = AlbumContainerViewModel.ConvertToViewModelDataList(modelData);
            }
            catch (Exception ex) {
                throw;
            }
        }
        private void SearchPhotos(object parameter) {
            try {
                SearchPhotosBLL bll = new SearchPhotosBLL();
                bll.Run();
                ReadFaceAlbums(null);
                if (_CurrentAlbumNum != -1)
                    ReadPhotos(_CurrentAlbumNum, _CurrentAlbumName);
            }
            catch (Exception ex) {
                throw;
            }
        }
        private void BackToAlbums(object parameter) {
            try {
                CurrentContentType = ContentType.FaceAlbum;
            }
            catch (Exception ex) {
                throw;
            }
        }
        public void ReadPhotos(int AlbumNum,string AlbumName) {
            try {
                _CurrentAlbumNum = AlbumNum;
                _CurrentAlbumName = AlbumName;
                var modelData = model.GetPhotoList(AlbumNum);
                Photos = PhotoContainerViewModel.ConvertToViewModelDataList(modelData);
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
    public class ContentTypeToFaceAlbumsShownFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((AlbumWindowViewModel.ContentType)value == AlbumWindowViewModel.ContentType.FaceAlbum) {
                    return System.Windows.Visibility.Visible;
                }
                else {
                    return System.Windows.Visibility.Collapsed;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class BoolToControlShownFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((bool)value) {
                    return System.Windows.Visibility.Visible;
                }
                else {
                    return System.Windows.Visibility.Collapsed;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ContentTypeToPhotosShownFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((AlbumWindowViewModel.ContentType)value == AlbumWindowViewModel.ContentType.Photo) {
                    return System.Windows.Visibility.Visible;
                }
                else {
                    return System.Windows.Visibility.Collapsed;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ContentTypeToBackButtonEnableFlgConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((AlbumWindowViewModel.ContentType)value == AlbumWindowViewModel.ContentType.Photo) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}