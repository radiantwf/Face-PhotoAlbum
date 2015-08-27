using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.Models {
    public class PhotoContainerModel : ObservableObject {
        public int PhotoNum { private set; get; }
        public string ImagePath { private set; get; }

        #region 数据获取
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <returns></returns>
        public static IList<PhotoContainerModel> GetPhotoList(int AlbumNum) {
            try {

                IList<PhotoContainerModel> list = new ObservableCollection<PhotoContainerModel>();
                FacePhotoAlbumContext context = new FacePhotoAlbumContext();
                var photoInfo = context.T_Face.Where(p => p.Status != 99).Join(context.T_AlbumLabel.Where(q => q.AlbumNum == AlbumNum)
                  , p => new { AlbumNum = p.ConfirmAlbumNum }
                  , q => new { AlbumNum = q.AlbumNum }
                  , (p, q) => new { PhotoNum = p.PhotoNum }).GroupBy(row => row.PhotoNum).Select(o => new { PhotoNum = o.Key }).Join(context.T_PhotoInfo.Where(p=>p.Status != 99)
                  , p => new { PhotoNum = p.PhotoNum }
                  , q => new { PhotoNum = q.PhotoNum }
                  , (p, q) => new { T_PhotoInfo = q });
                int count = photoInfo.Count();
                foreach(var row in photoInfo) {
                    PhotoContainerModel newRow = new PhotoContainerModel();
                    newRow.PhotoNum = row.T_PhotoInfo.PhotoNum;
                    newRow.ImagePath = Path.Combine(row.T_PhotoInfo.FilePath, row.T_PhotoInfo.FileName);
                    list.Add(newRow);
                }
                return list;
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}
