namespace Trippy_Land.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PNV012 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BaiVietVeDiaDiems", "NoiDungBaiViet", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BaiVietVeDiaDiems", "NoiDungBaiViet", c => c.String(maxLength: 4000));
        }
    }
}
