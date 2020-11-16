namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV5 : DbMigration
    {
        public override void Up()
        {
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

        }

        public override void Down()
        {
            DropForeignKey("dbo.DanhGiaBaiViets", "IdDanhGia", "dbo.DanhGia");
            DropForeignKey("dbo.DanhGiaBaiViets", "IdBaiBiet", "dbo.BaiVietVeDiaDiems");
            DropIndex("dbo.DanhGiaBaiViets", new[] { "IdDanhGia" });
            DropIndex("dbo.DanhGiaBaiViets", new[] { "IdBaiBiet" });
            DropTable("dbo.DanhGiaBaiViets");
        }
    }
}
