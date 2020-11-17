using System.Data.Entity;

namespace Trippy_Land.Models
{
    public class Trippy_Land_Context : DbContext
    {
        public Trippy_Land_Context() : base("Trippy_Land_Connect")
        {
        }


        public DbSet<ChuDeBaiVietVeDiaDiem> ChuDeBaiVietVeDiaDiems { get; set; }
        public DbSet<BaiVietVeDiaDiem> BaiVietVeDiaDiems { get; set; }
        public DbSet<DiaDiem> DiaDiems { get; set; }
        public DbSet<KhachSan> KhachSans { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<Tinh> Tinhs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
    }
}