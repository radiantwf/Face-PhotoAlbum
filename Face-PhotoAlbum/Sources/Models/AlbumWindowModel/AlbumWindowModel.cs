using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_PhotoAlbum.Models {
    public class AlbumWindowModel : ObservableObject {
        /// <summary>
        /// 获取相册数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FaceAlbumModel> GetFaceAlbumList() {

            //Business1 bll = new Business1();
            //bll.Run();
            return FaceAlbumModel.GetFaceAlbumList();
        }
        public IEnumerable<PhotoModel> GetPhotoList(int albumNum) {

            return PhotoModel.GetPhotoList(albumNum);
        }

    }
}
