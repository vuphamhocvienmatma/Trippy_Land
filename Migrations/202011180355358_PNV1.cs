namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.User", "IsSupper", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogHeThongs", "UserId", "dbo.User");
            DropIndex("dbo.LogHeThongs", new[] { "UserId" });
            DropColumn("dbo.User", "IsSupper");
            DropTable("dbo.LogHeThongs");
        }
    }
}
