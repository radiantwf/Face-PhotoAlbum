using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.Models {
    public class PhotoContainerModel  {
        public enum MatchTypeType { Unknown, Possible, Confirm }
        public int PhotoNum { private set; get; }
        public string ImagePath { private set; get; }
        public MatchTypeType MatchType { private set; get; }

        #region 数据获取
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <returns></returns>
        public static IList<PhotoContainerModel> GetPhotoList(int AlbumNum) {
            try {

                IList<PhotoContainerModel> list = new ObservableCollection<PhotoContainerModel>();
                FacePhotoAlbumContext context = new FacePhotoAlbumContext();


                var confirmMatchPhotosInfo = context.T_Face.Where(p => p.Status != 99 &&
                    p.ConfirmAlbumNum == AlbumNum)
                    .GroupBy(row => row.PhotoNum).Select(o => new { PhotoNum = o.Key }).Join(context.T_PhotoInfo.Where(p => p.Status != 99)
                    , p => new { PhotoNum = p.PhotoNum }
                    , q => new { PhotoNum = q.PhotoNum }
                    , (p, q) => new { T_PhotoInfo = q });
                foreach (var row in confirmMatchPhotosInfo) {
                    PhotoContainerModel newRow = new PhotoContainerModel();
                    newRow.PhotoNum = row.T_PhotoInfo.PhotoNum;
                    newRow.MatchType = MatchTypeType.Confirm;
                    newRow.ImagePath = Path.Combine(row.T_PhotoInfo.FilePath, row.T_PhotoInfo.FileName);
                    list.Add(newRow);
                }

                string strAlbumNum = AlbumNum.ToString();
                var possibleMatchPhotosInfo = context.T_Face.Where(p => p.Status != 99 &&
                    (p.ConfirmAlbumNum == -1 && (p.PossibleAlbumNum.StartsWith(strAlbumNum + ",") || p.PossibleAlbumNum.Contains("," + strAlbumNum + ",") || p.PossibleAlbumNum.EndsWith(strAlbumNum + ",") || p.PossibleAlbumNum == strAlbumNum)))
                    .GroupBy(row => row.PhotoNum).Select(o => new { PhotoNum = o.Key }).Join(context.T_PhotoInfo.Where(p => p.Status != 99)
                    , p => new { PhotoNum = p.PhotoNum }
                    , q => new { PhotoNum = q.PhotoNum }
                    , (p, q) => new { T_PhotoInfo = q }).Except(confirmMatchPhotosInfo);
                foreach (var row in possibleMatchPhotosInfo) {
                    PhotoContainerModel newRow = new PhotoContainerModel();
                    newRow.PhotoNum = row.T_PhotoInfo.PhotoNum;
                    newRow.MatchType = MatchTypeType.Possible;
                    newRow.ImagePath = Path.Combine(row.T_PhotoInfo.FilePath, row.T_PhotoInfo.FileName);
                    list.Add(newRow);
                }

                if (AlbumNum == 0) {
                    var unknownMatchPhotosInfo = context.T_Face.Where(p => p.Status != 99 &&
                        p.ConfirmAlbumNum == -1)
                        .GroupBy(row => row.PhotoNum).Select(o => new { PhotoNum = o.Key }).Join(context.T_PhotoInfo.Where(p => p.Status != 99)
                        , p => new { PhotoNum = p.PhotoNum }
                        , q => new { PhotoNum = q.PhotoNum }
                        , (p, q) => new { T_PhotoInfo = q });
                    foreach (var row in unknownMatchPhotosInfo) {
                        PhotoContainerModel newRow = new PhotoContainerModel();
                        newRow.PhotoNum = row.T_PhotoInfo.PhotoNum;
                        newRow.MatchType = MatchTypeType.Unknown;
                        newRow.ImagePath = Path.Combine(row.T_PhotoInfo.FilePath, row.T_PhotoInfo.FileName);
                        list.Add(newRow);
                    }
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
