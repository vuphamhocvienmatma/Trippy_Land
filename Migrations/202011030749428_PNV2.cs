namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiaDiem", "TieuDe", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiaDiem", "TieuDe");
        }
    }
}
