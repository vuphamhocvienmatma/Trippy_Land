namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV011 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuDeBaiVietVeDiaDiems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenChuDe = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BaiVietVeDiaDiems", "IdChude", c => c.Int(nullable: false));
            CreateIndex("dbo.BaiVietVeDiaDiems", "IdChude");
            AddForeignKey("dbo.BaiVietVeDiaDiems", "IdChude", "dbo.ChuDeBaiVietVeDiaDiems", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BaiVietVeDiaDiems", "IdChude", "dbo.ChuDeBaiVietVeDiaDiems");
            DropIndex("dbo.BaiVietVeDiaDiems", new[] { "IdChude" });
            DropColumn("dbo.BaiVietVeDiaDiems", "IdChude");
            DropTable("dbo.ChuDeBaiVietVeDiaDiems");
        }
    }
}
