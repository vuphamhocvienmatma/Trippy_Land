namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaiVietVeDiaDiems", "DataCreated", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.BaiVietVeDiaDiems", "DataCreated");
        }
    }
}
