using Face_PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
    public class AlbumContainerViewModel : ObservableObject {
        private string _AlbumLabel;
        private bool _IsSelected = false;
        private int _ImageCount;
        private static ObservableCollection<AlbumContainerViewModel> _FaceAlbumViewModelList;

        private ICommand _SelectAlbumCommand;
        public ICommand SelectAlbumCommand {
            get {
                if (this._SelectAlbumCommand == null) {
                    this._SelectAlbumCommand = new CommandProxy(SelectAlbum);
                }
                return this._SelectAlbumCommand;
            }
        }
        private void SelectAlbum(object parameter) {
            try {
                foreach (var Element in _FaceAlbumViewModelList) {
                    if (Element != this && Element.IsSelected) {
                        Element.IsSelected = false;
                    }
                }
                IsSelected = true;
            }
            catch (Exception ex) {
                throw;
            }
        }

        private ICommand _EnterAlbumCommand;
        public ICommand EnterAlbumCommand {
            get {
                if (this._EnterAlbumCommand == null) {
                    this._EnterAlbumCommand = new CommandProxy(EnterAlbum);
                }
                return this._EnterAlbumCommand;
            }
        }
        private void EnterAlbum(object parameter) {
            try {
                var albumWindowViewModel = parameter as AlbumWindowViewModel;
                albumWindowViewModel.ReadPhotos(AlbumNum, _AlbumLabel);
                albumWindowViewModel.CurrentContentType = AlbumWindowViewModel.ContentType.Photo;
            }
            catch (Exception ex) {
                throw;
            }
        }

        public int AlbumNum { get; private set; }
        public byte[] CoverImage { get; private set; }
        public string ImageCountStr {
            get {
                return AlbumNum != 0 ? "共有" + _ImageCount.ToString() + "张照片" : null;
            }
        }

        public string AlbumLabel {
            get {
                return _AlbumLabel;
            }
            set {
                _AlbumLabel = value;
                RaisePropertyChanged(() => AlbumLabel);
            }
        }
        public bool IsSelected {
            get {
                return _IsSelected;
            }
            set {
                if (_IsSelected == value)
                    return;
                _IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #region 数据获取
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<AlbumContainerViewModel> ConvertToViewModelDataList(IEnumerable<AlbumContainerModel> model) {
            _FaceAlbumViewModelList = new ObservableCollection<AlbumContainerViewModel>();
            model.ToList().ForEach(row => {
                AlbumContainerViewModel faceViewModel = new AlbumContainerViewModel();
                faceViewModel._AlbumLabel = row.AlbumLabel;
                faceViewModel.AlbumNum = row.AlbumNum;
                faceViewModel.CoverImage = row.CoverImage;
                faceViewModel._ImageCount = row.ImageCount;
                faceViewModel.IsSelected = false;
                _FaceAlbumViewModelList.Add(faceViewModel);
            });

            return _FaceAlbumViewModelList;
        }
        #endregion
    }

    public class FaceAlbumSelectStatusToBgPathConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((bool)value) {
                    return @"/Resources/相册背景selected.png";
                }
                else {
                    return @"/Resources/相册背景.png";
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
    public class FaceAlbumNullImageToResourceImageConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if (value == null) {
                    return @"/Resources/未知.png";
                }
                else {
                    return value;
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
