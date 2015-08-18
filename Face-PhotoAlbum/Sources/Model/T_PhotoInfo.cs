using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_PhotoAlbum.Sources.Model {
    public class T_PhotoInfo {
        [Key, Column(Order = 0)]
        public int PhotoNum { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string MD5 { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
