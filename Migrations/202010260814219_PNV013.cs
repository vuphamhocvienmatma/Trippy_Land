namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV013 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "TenNguoiDung", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "TenNguoiDung", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
