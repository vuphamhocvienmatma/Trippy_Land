namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "EmailConfirm", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "IsValid", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsValid");
            DropColumn("dbo.User", "EmailConfirm");
        }
    }
}
