namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PNV01 : DbMigration
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
                    NoiDungBaiViet = c.String(maxLength: 4000),
                    idDiaDiem = c.Int(nullable: false),
                    PictureId = c.String(maxLength: 500),
                    DaDuyet = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiaDiem", t => t.idDiaDiem, cascadeDelete: true)
                .Index(t => t.idDiaDiem);

            CreateTable(
                "dbo.DiaDiem",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenDiaDiem = c.String(nullable: false, maxLength: 200),
                    PictureId = c.String(maxLength: 500),
                    HoatDongChinh = c.String(nullable: false, maxLength: 4000),
                    KinhNghiem = c.String(nullable: false, maxLength: 4000),
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
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DanhGia",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    NoiDungDanhGia = c.String(maxLength: 4000),
                    idTinh = c.Int(nullable: false),
                    idUser = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tinh", t => t.idTinh, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idTinh)
                .Index(t => t.idUser);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenDangNhap = c.String(nullable: false, maxLength: 50),
                    MatKhau = c.String(nullable: false, maxLength: 8000, unicode: false),
                    TenNguoiDung = c.String(nullable: false, maxLength: 50),
                    PhoneNumber = c.Int(nullable: false),
                    Email = c.String(maxLength: 50, unicode: false),
                    GioiTinh = c.String(maxLength: 4000),
                    PictureId = c.String(maxLength: 500),
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
            DropForeignKey("dbo.DanhGia", "idUser", "dbo.User");
            DropForeignKey("dbo.DanhGia", "idTinh", "dbo.Tinh");
            DropForeignKey("dbo.BaiVietVeDiaDiems", "idDiaDiem", "dbo.DiaDiem");
            DropIndex("dbo.MonAn", new[] { "idTinh" });
            DropIndex("dbo.KhachSan", new[] { "idTinh" });
            DropIndex("dbo.DanhGia", new[] { "idUser" });
            DropIndex("dbo.DanhGia", new[] { "idTinh" });
            DropIndex("dbo.DiaDiem", new[] { "idTinh" });
            DropIndex("dbo.BaiVietVeDiaDiems", new[] { "idDiaDiem" });
            DropTable("dbo.MonAn");
            DropTable("dbo.KhachSan");
            DropTable("dbo.User");
            DropTable("dbo.DanhGia");
            DropTable("dbo.Tinh");
            DropTable("dbo.DiaDiem");
            DropTable("dbo.BaiVietVeDiaDiems");
        }
    }
}