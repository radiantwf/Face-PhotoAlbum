using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.ViewModel {
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
}
