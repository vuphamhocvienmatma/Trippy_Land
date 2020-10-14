namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV011 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tinh", "PictureId", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tinh", "PictureId", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
