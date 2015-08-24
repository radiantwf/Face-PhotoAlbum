using System.Data.Entity;

namespace Face_PhotoAlbum.Models {
    public class FacePhotoAlbumContext : DbContext {
        public DbSet<T_PhotoInfo> T_PhotoInfo { get; set; }
        public DbSet<T_Face> T_Face { get; set; }
        public DbSet<T_FaceComparison> T_FaceComparison { get; set; }
        public DbSet<T_AlbumLabel> T_AlbumLabel { get; set; }
    }
}
