using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_PhotoAlbum.Model {
    public class T_FaceLabel {
        [Key, Column(Order = 0)]
        public int LabelNum { get; set; }
        public string FaceLabel { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
