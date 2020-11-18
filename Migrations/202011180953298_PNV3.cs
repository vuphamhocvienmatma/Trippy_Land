namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Functions", "NgayTao");
            DropColumn("dbo.Functions", "OrderNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Functions", "OrderNum", c => c.Int(nullable: false));
            AddColumn("dbo.Functions", "NgayTao", c => c.DateTime(nullable: false));
        }
    }
}
