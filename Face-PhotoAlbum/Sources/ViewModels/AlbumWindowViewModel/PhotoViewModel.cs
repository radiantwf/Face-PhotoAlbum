using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Face_PhotoAlbum.ViewModels {
        public class PhotoViewModel : ObservableObject {
            private string _AlbumLabel;
            private bool _IsSelected = false;
            private int _ImageCount;
            private static ObservableCollection<PhotoViewModel> _PhotoViewModelList;
            private ICommand _SelectAlbumCommand;
            public ICommand SelectAlbumCommand {
                get {
                    if (this._SelectAlbumCommand == null) {
                        this._SelectAlbumCommand = new CommandProxy(SelectAlbum);
                    }
                    return this._SelectAlbumCommand;
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
            private void SelectAlbum(object parameter) {
                try {
                    foreach (var Element in _PhotoViewModelList) {
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
            private void EnterAlbum(object parameter) {
                try {
                    var tmp = parameter as AlbumWindowViewModel;
                    if (tmp.Photos == _PhotoViewModelList) {

                    }
                }
                catch (Exception ex) {
                    throw;
                }
            }

            public int AlbumNum { get; private set; }
            public byte[] CoverImage { get; private set; }
            public string ImageCountStr {
                get {
                    return "共有" + _ImageCount.ToString() + "张照片";
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
            public static ObservableCollection<PhotoViewModel> ConvertToViewModelDataList(IEnumerable<PhotoModel> model) {
                _PhotoViewModelList = new ObservableCollection<PhotoViewModel>();
                model.ToList().ForEach(row => {
                    PhotoViewModel faceViewModel = new PhotoViewModel();
                    faceViewModel._AlbumLabel = row.AlbumLabel;
                    faceViewModel.AlbumNum = row.AlbumNum;
                    faceViewModel.CoverImage = row.CoverImage;
                    faceViewModel._ImageCount = row.ImageCount;
                    faceViewModel.IsSelected = false;
                    _PhotoViewModelList.Add(faceViewModel);
                });

                return _PhotoViewModelList;
            }
            #endregion
        }

        public class PhotoSelectStatusToBgPathConverter : IValueConverter {

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
        public class PhotoNullImageToResourceImageConverter : IValueConverter {

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
