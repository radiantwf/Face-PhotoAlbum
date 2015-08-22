using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_PhotoAlbum.Model {
    public class T_AlbumLabel {
        [Key, Column(Order = 0)]
        public int AlbumNum { get; set; }
        public string AlbumLabel { get; set; }
        public byte[] CoverImage { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
