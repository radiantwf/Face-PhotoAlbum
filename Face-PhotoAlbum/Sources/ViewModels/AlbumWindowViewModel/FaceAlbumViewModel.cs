using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Face_PhotoAlbum.ViewModels {
    public class FaceAlbumViewModel : ObservableObject {
        public int FaceAlbumName { get; set; }
        public byte[] CoverImage { get; set; }
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

        //#region 模拟数据获取
        ///// <summary>
        ///// 模拟测试数据
        ///// </summary>
        ///// <returns></returns>
        //public static ObservableCollection<FaceAlbumViewModel> GetFaceAlbumList()
        //{
        //    ObservableCollection<FaceAlbumViewModel> list = new ObservableCollection<FaceAlbumViewModel>();

        //    return list;
        //}
        //#endregion
    }

    public class FaceAlbumSelectStatusToBgPathConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if ((bool)value) {
                return @"/Resources/场景背景selected.png";
            }
            else {
                return @"/Resources/场景背景.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
