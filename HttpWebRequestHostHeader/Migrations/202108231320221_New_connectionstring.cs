namespace HttpWebRequestHostHeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_connectionstring : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IpCheck");
            AlterColumn("dbo.IpCheck", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddPrimaryKey("dbo.IpCheck", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IpCheck");
            AlterColumn("dbo.IpCheck", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.IpCheck", "Id");
        }
    }
}
