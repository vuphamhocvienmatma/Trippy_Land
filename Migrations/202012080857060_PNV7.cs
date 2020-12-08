namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LogHeThongs", "UserId", "dbo.User");
            DropIndex("dbo.LogHeThongs", new[] { "UserId" });
            DropTable("dbo.LogHeThongs");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.LogHeThongs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MoTa = c.String(maxLength: 4000),
                    HanhDongId = c.Int(nullable: false),
                    FormName = c.String(),
                    NguoiTao = c.String(),
                    DiaChiMayTinh = c.String(),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.LogHeThongs", "UserId");
            AddForeignKey("dbo.LogHeThongs", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
