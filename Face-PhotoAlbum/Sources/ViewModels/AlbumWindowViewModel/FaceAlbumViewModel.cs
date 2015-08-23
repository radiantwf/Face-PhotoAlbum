using Face_PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Face_PhotoAlbum.ViewModels {
    public class FaceAlbumViewModel : ObservableObject {
        private string _AlbumLabel;
        private bool _IsSelected = false;

        public int AlbumNum { get; private set; }
        public byte[] CoverImage { get; private set; }
        public int ImageCount { get; private set; }

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
                FaceAlbumViewModel faceViewModel = new FaceAlbumViewModel();
                faceViewModel._AlbumLabel = row.AlbumLabel;
                faceViewModel.AlbumNum = row.AlbumNum;
                faceViewModel.CoverImage = row.CoverImage == null ? File.ReadAllBytes(@"Resources\未知.png") : row.CoverImage;
                faceViewModel.ImageCount = row.ImageCount;
                faceViewModel.IsSelected = false;
                list.Add(faceViewModel);
            });

            return list;
        }
        #endregion
    }

    public class FaceAlbumSelectStatusToBgPathConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value) {
                return @"../../../Resources/场景背景selected.png";
            }
            else {
                return @"../../../Resources/场景背景.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
