namespace HttpWebRequestHostHeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checker_IP_First_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CheckType = c.String(nullable: false, maxLength: 1, unicode: false),
                        CheckName = c.String(nullable: false, maxLength: 50, unicode: false),
                        CheckDesc = c.String(maxLength: 3000, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.CheckType, t.CheckName });
            
            CreateTable(
                "dbo.IpCheck",
                c => new
                    {
                        ReqTime = c.DateTime(nullable: false),
                        IpAddr = c.String(nullable: false, maxLength: 15, unicode: false),
                        ResSpan = c.Int(),
                        CheckType = c.String(nullable: false, maxLength: 1, unicode: false),
                        ResCode = c.String(maxLength: 3, unicode: false),
                        ContentLength = c.Int(),
                    })
                .PrimaryKey(t => new { t.ReqTime, t.IpAddr });
            
            CreateTable(
                "dbo.Params",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                        Value = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Params");
            DropTable("dbo.IpCheck");
            DropTable("dbo.CheckType");
        }
    }
}
