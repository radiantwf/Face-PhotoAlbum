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

namespace Face_PhotoAlbum.ViewModels {
    public class FaceAlbumViewModel : ObservableObject {
        private string _AlbumLabel;
        private bool _IsSelected = false;
        private int _ImageCount;

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
                _IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #region 数据获取
        /// <summary>
        /// 模拟测试数据
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FaceAlbumViewModel> ConvertToViewModelDataList(IEnumerable<FaceAlbumModel> model) {
            ObservableCollection<FaceAlbumViewModel> list = new ObservableCollection<FaceAlbumViewModel>();
            model.ToList().ForEach(row => {
                if (row.AlbumNum == 0)
                    return;
                FaceAlbumViewModel faceViewModel = new FaceAlbumViewModel();
                faceViewModel._AlbumLabel = row.AlbumLabel;
                faceViewModel.AlbumNum = row.AlbumNum;
                faceViewModel.CoverImage = row.CoverImage;
                faceViewModel._ImageCount = row.ImageCount;
                faceViewModel.IsSelected = false;
                list.Add(faceViewModel);
            });

            return list;
        }
        #endregion
    }

    public class FaceAlbumSelectStatusToBgPathConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if ((bool)value) {
                    //return @"../../../Resources/场景背景selected.png";
                    return @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/场景背景selected.png";
                }
                else {
                    //return @"../../../Resources/场景背景.png";
                    return @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/场景背景.png";
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
                    return new Uri("pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/未知.png"); ;
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
