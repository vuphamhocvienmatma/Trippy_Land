namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaiVietVeDiaDiems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenBaiViet = c.String(nullable: false, maxLength: 200),
                    TomTatBaiViet = c.String(maxLength: 4000),
                    NoiDungBaiViet = c.String(),
                    idDiaDiem = c.Int(nullable: false),
                    PictureId = c.String(maxLength: 500),
                    DaDuyet = c.Boolean(nullable: false),
                    DataCreated = c.DateTime(nullable: false),
                    IdChude = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChuDeBaiVietVeDiaDiems", t => t.IdChude, cascadeDelete: true)
                .ForeignKey("dbo.DiaDiem", t => t.idDiaDiem, cascadeDelete: true)
                .Index(t => t.idDiaDiem)
                .Index(t => t.IdChude);

            CreateTable(
                "dbo.ChuDeBaiVietVeDiaDiems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenChuDe = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DanhGiaBaiViets",
                c => new
                {
                    IdBaiBiet = c.Int(nullable: false),
                    IdDanhGia = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.IdBaiBiet, t.IdDanhGia })
                .ForeignKey("dbo.BaiVietVeDiaDiems", t => t.IdBaiBiet, cascadeDelete: true)
                .ForeignKey("dbo.DanhGia", t => t.IdDanhGia, cascadeDelete: true)
                .Index(t => t.IdBaiBiet)
                .Index(t => t.IdDanhGia);

            CreateTable(
                "dbo.DanhGia",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    NoiDungDanhGia = c.String(maxLength: 4000),
                    idUser = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idUser);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenDangNhap = c.String(nullable: false, maxLength: 50),
                    MatKhau = c.String(nullable: false, maxLength: 8000, unicode: false),
                    TenNguoiDung = c.String(maxLength: 50),
                    PhoneNumber = c.Int(nullable: false),
                    Email = c.String(maxLength: 50, unicode: false),
                    GioiTinh = c.String(maxLength: 4000),
                    PictureId = c.String(maxLength: 500),
                    UserRoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);

            CreateTable(
                "dbo.UserRoles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenRole = c.String(nullable: false, maxLength: 50, unicode: false),
                    MoTa = c.String(maxLength: 500),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserRoleAndFunctions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Xem = c.Boolean(nullable: false),
                    Them = c.Boolean(nullable: false),
                    Sua = c.Boolean(nullable: false),
                    Xoa = c.Boolean(nullable: false),
                    UserRoleId = c.Int(nullable: false),
                    FuctionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Functions", t => t.FuctionId, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId)
                .Index(t => t.FuctionId);

            CreateTable(
                "dbo.Functions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenChucNang = c.String(maxLength: 50),
                    MoTa = c.String(maxLength: 50),
                    TenForm = c.String(maxLength: 50),
                    NgayTao = c.DateTime(nullable: false),
                    OrderNum = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DiaDiem",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenDiaDiem = c.String(nullable: false, maxLength: 200),
                    PictureId = c.String(maxLength: 500),
                    HoatDongChinh = c.String(nullable: false, maxLength: 4000),
                    KinhNghiem = c.String(nullable: false, maxLength: 4000),
                    TieuDe = c.String(nullable: false, maxLength: 4000),
                    idTinh = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tinh", t => t.idTinh, cascadeDelete: true)
                .Index(t => t.idTinh);

            CreateTable(
                "dbo.Tinh",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenTinh = c.String(nullable: false, maxLength: 200),
                    PictureId = c.String(maxLength: 500),
                    ChiTiet = c.String(maxLength: 4000),
                    TomTat = c.String(maxLength: 500),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.KhachSan",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenKhachSan = c.String(nullable: false, maxLength: 200),
                    PictureId = c.String(maxLength: 500),
                    ChiTiet = c.String(maxLength: 4000),
                    LichSuKhachSan = c.String(maxLength: 4000),
                    DiaDiemChiTiet = c.String(maxLength: 4000),
                    Price = c.Int(nullable: false),
                    idTinh = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tinh", t => t.idTinh, cascadeDelete: true)
                .Index(t => t.idTinh);

            CreateTable(
                "dbo.MonAn",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenMonAn = c.String(nullable: false, maxLength: 200),
                    PictureId = c.String(maxLength: 500),
                    ChiTiet = c.String(maxLength: 4000),
                    LichSuaMonAn = c.String(maxLength: 4000),
                    NguyenLieu = c.String(maxLength: 4000),
                    CachLam = c.String(maxLength: 4000),
                    idTinh = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tinh", t => t.idTinh, cascadeDelete: true)
                .Index(t => t.idTinh);

        }

        public override void Down()
        {
            DropForeignKey("dbo.MonAn", "idTinh", "dbo.Tinh");
            DropForeignKey("dbo.KhachSan", "idTinh", "dbo.Tinh");
            DropForeignKey("dbo.DiaDiem", "idTinh", "dbo.Tinh");
            DropForeignKey("dbo.BaiVietVeDiaDiems", "idDiaDiem", "dbo.DiaDiem");
            DropForeignKey("dbo.User", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoleAndFunctions", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoleAndFunctions", "FuctionId", "dbo.Functions");
            DropForeignKey("dbo.DanhGia", "idUser", "dbo.User");
            DropForeignKey("dbo.DanhGiaBaiViets", "IdDanhGia", "dbo.DanhGia");
            DropForeignKey("dbo.DanhGiaBaiViets", "IdBaiBiet", "dbo.BaiVietVeDiaDiems");
            DropForeignKey("dbo.BaiVietVeDiaDiems", "IdChude", "dbo.ChuDeBaiVietVeDiaDiems");
            DropIndex("dbo.MonAn", new[] { "idTinh" });
            DropIndex("dbo.KhachSan", new[] { "idTinh" });
            DropIndex("dbo.DiaDiem", new[] { "idTinh" });
            DropIndex("dbo.UserRoleAndFunctions", new[] { "FuctionId" });
            DropIndex("dbo.UserRoleAndFunctions", new[] { "UserRoleId" });
            DropIndex("dbo.User", new[] { "UserRoleId" });
            DropIndex("dbo.DanhGia", new[] { "idUser" });
            DropIndex("dbo.DanhGiaBaiViets", new[] { "IdDanhGia" });
            DropIndex("dbo.DanhGiaBaiViets", new[] { "IdBaiBiet" });
            DropIndex("dbo.BaiVietVeDiaDiems", new[] { "IdChude" });
            DropIndex("dbo.BaiVietVeDiaDiems", new[] { "idDiaDiem" });
            DropTable("dbo.MonAn");
            DropTable("dbo.KhachSan");
            DropTable("dbo.Tinh");
            DropTable("dbo.DiaDiem");
            DropTable("dbo.Functions");
            DropTable("dbo.UserRoleAndFunctions");
            DropTable("dbo.UserRoles");
            DropTable("dbo.User");
            DropTable("dbo.DanhGia");
            DropTable("dbo.DanhGiaBaiViets");
            DropTable("dbo.ChuDeBaiVietVeDiaDiems");
            DropTable("dbo.BaiVietVeDiaDiems");
        }
    }
}
