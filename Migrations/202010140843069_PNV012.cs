namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV012 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DiaDiem", "PictureId", c => c.String(maxLength: 500));
            AlterColumn("dbo.KhachSan", "PictureId", c => c.String(maxLength: 500));
            AlterColumn("dbo.MonAn", "PictureId", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MonAn", "PictureId", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.KhachSan", "PictureId", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.DiaDiem", "PictureId", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
