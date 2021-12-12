namespace HttpWebRequestHostHeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class All_OK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IpCheck");
            AddColumn("dbo.IpCheck", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddPrimaryKey("dbo.IpCheck", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IpCheck");
            DropColumn("dbo.IpCheck", "Id");
            AddPrimaryKey("dbo.IpCheck", new[] { "ReqTime", "IpAddr" });
        }
    }
}
