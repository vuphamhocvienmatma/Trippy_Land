namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tinh", "TomTat", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tinh", "TomTat");
        }
    }
}
