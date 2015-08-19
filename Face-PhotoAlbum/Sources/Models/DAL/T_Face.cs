using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_PhotoAlbum.Model {
    public class T_Face {
        [Key, Column(Order = 0)]
        public int PhotoNum { get; set; }
        [Key, Column(Order = 1)]
        public int SequenceNum { get; set; }
        public int RectLeft { get; set; }
        public int RectRight { get; set; }
        public int RectTop { get; set; }
        public int RectBottom { get; set; }
        public string FaceFileName { get; set; }
        public string FaceFilePath { get; set; }
        public byte[] FeatureData { get; set; }
        public int ConfirmLabelNum { get; set; }
        public int PossibleLabelNum { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
