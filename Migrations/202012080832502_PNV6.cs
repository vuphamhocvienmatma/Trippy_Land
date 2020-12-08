namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PNV6 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.DanhGiaBaiViets");
            DropTable("dbo.DanhGia");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.DanhGia",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    NoiDungDanhGia = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DanhGiaBaiViets",
                c => new
                {
                    IdBaiBiet = c.Int(nullable: false),
                    IdDanhGia = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.IdBaiBiet, t.IdDanhGia });

        }
    }
}
