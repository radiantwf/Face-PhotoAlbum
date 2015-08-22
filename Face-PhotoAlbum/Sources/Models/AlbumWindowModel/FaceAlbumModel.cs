using Face_PhotoAlbum.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.Models {
    public class FaceAlbumModel : ObservableObject {
        private string _AlbumLabel;
        private bool _IsSelected = false;


        public int AlbumNum { get; private set; }
        public byte[] CoverImage { get; private set; }
        public int ImageCount { get; private set; }


        public bool IsSelected {
            get {
                return _IsSelected;
            }
            set {
                _IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
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

        #region 数据获取
        /// <summary>
        /// 模拟测试数据
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FaceAlbumModel> GetFaceAlbumList() {
            ObservableCollection<FaceAlbumModel> list = new ObservableCollection<FaceAlbumModel>();
            FacePhotoAlbumContext context = new FacePhotoAlbumContext();
            context.T_AlbumLabel.OrderBy(p => p.AlbumNum).ToList().ForEach(row => {
                FaceAlbumModel faceAlbum = new FaceAlbumModel();
                faceAlbum.AlbumNum = row.AlbumNum;
                faceAlbum._AlbumLabel = row.AlbumLabel;
                faceAlbum.CoverImage = row.CoverImage;
                faceAlbum._IsSelected = false;
                faceAlbum.ImageCount = context.T_Face.Where(p => p.ConfirmAlbumNum == row.AlbumNum || p.PossibleAlbumNum.Split(',').Contains(row.AlbumNum.ToString())).GroupBy(p => p.PhotoNum).Count();

            });

            return list;
        }
        #endregion
    }
}
