namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRoleAndFunctions", "Xem");
            DropColumn("dbo.UserRoleAndFunctions", "Them");
            DropColumn("dbo.UserRoleAndFunctions", "Sua");
            DropColumn("dbo.UserRoleAndFunctions", "Xoa");
        }

        public override void Down()
        {
            AddColumn("dbo.UserRoleAndFunctions", "Xoa", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoleAndFunctions", "Sua", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoleAndFunctions", "Them", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoleAndFunctions", "Xem", c => c.Boolean(nullable: false));
        }
    }
}
