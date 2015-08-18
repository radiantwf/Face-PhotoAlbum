using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_PhotoAlbum.Model {
    public class T_FaceComparison {
        [Key, Column(Order = 0)]
        public int Num { get; set; }
        public int PhotoNum1 { get; set; }
        public int SequenceNum1 { get; set; }
        public int PhotoNum2 { get; set; }
        public int SequenceNum2 { get; set; }
        public float Score { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
