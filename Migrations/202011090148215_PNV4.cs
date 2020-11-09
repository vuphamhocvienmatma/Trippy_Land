namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DanhGia", "idTinh", "dbo.Tinh");
            DropIndex("dbo.DanhGia", new[] { "idTinh" });
            DropColumn("dbo.DanhGia", "idTinh");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DanhGia", "idTinh", c => c.Int(nullable: false));
            CreateIndex("dbo.DanhGia", "idTinh");
            AddForeignKey("dbo.DanhGia", "idTinh", "dbo.Tinh", "Id", cascadeDelete: true);
        }
    }
}
