using Face_PhotoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.Models {
    public class FaceAlbumModel : ObservableObject {
        private string _AlbumLabel;


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

        #region 数据获取
        /// <summary>
        /// 模拟测试数据
        /// </summary>
        /// <returns></returns>
        public static IList<FaceAlbumModel> GetFaceAlbumList() {
            try {

                IList<FaceAlbumModel> list = new ObservableCollection<FaceAlbumModel>();
                FacePhotoAlbumContext context = new FacePhotoAlbumContext();
                context.T_AlbumLabel.OrderBy(p => p.AlbumNum).ToList().ForEach(row => {
                    FaceAlbumModel faceAlbum = new FaceAlbumModel();
                    faceAlbum.AlbumNum = row.AlbumNum;
                    faceAlbum._AlbumLabel = row.AlbumLabel;
                    faceAlbum.CoverImage = row.CoverImage;
                    string strAlbumNum = row.AlbumNum.ToString();
                    faceAlbum.ImageCount = context.T_Face.Where(p => p.ConfirmAlbumNum == row.AlbumNum || p.PossibleAlbumNum.StartsWith(strAlbumNum + ",") || p.PossibleAlbumNum.EndsWith("," + strAlbumNum ) || p.PossibleAlbumNum.Contains(","+strAlbumNum + ",")).GroupBy(p => p.PhotoNum).Count();
                    list.Add(faceAlbum);
                });

                return list;
            }
            catch (Exception){
                throw;
            }
        }
        #endregion
    }
}
