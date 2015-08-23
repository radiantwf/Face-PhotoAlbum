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
            return FaceAlbumModel.GetFaceAlbumList();
        }

    }
}
