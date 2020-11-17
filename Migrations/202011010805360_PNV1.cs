namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachSan", "Price", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.KhachSan", "Price");
        }
    }
}
