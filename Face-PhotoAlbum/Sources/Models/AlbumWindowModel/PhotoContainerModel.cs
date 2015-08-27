using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.Models {
    public class PhotoContainerModel : ObservableObject {


        #region 数据获取
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <returns></returns>
        public static IList<PhotoContainerModel> GetPhotoList(int AlbumNum) {
            try {

                IList<PhotoContainerModel> list = new ObservableCollection<PhotoContainerModel>();
                FacePhotoAlbumContext context = new FacePhotoAlbumContext();
                //var tmp = context.T_PhotoInfo.Join(context.T_Face.Join())
                return list;
            }
            catch (Exception) {
                throw;
            }
        }
        #endregion
    }
}
