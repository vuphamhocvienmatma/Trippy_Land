namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DanhGiaBaiViets", "IdBaiBiet", "dbo.BaiVietVeDiaDiems");
            DropForeignKey("dbo.DanhGiaBaiViets", "IdDanhGia", "dbo.DanhGia");
            DropForeignKey("dbo.DanhGia", "idUser", "dbo.User");
            DropIndex("dbo.DanhGiaBaiViets", new[] { "IdBaiBiet" });
            DropIndex("dbo.DanhGiaBaiViets", new[] { "IdDanhGia" });
            DropIndex("dbo.DanhGia", new[] { "idUser" });
            DropColumn("dbo.DanhGia", "idUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DanhGia", "idUser", c => c.Int(nullable: false));
            CreateIndex("dbo.DanhGia", "idUser");
            CreateIndex("dbo.DanhGiaBaiViets", "IdDanhGia");
            CreateIndex("dbo.DanhGiaBaiViets", "IdBaiBiet");
            AddForeignKey("dbo.DanhGia", "idUser", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DanhGiaBaiViets", "IdDanhGia", "dbo.DanhGia", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DanhGiaBaiViets", "IdBaiBiet", "dbo.BaiVietVeDiaDiems", "Id", cascadeDelete: true);
        }
    }
}
