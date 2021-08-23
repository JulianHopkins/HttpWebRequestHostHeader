namespace HttpWebRequestHostHeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeinttolonginIpCheck : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IpCheck", "ResSpan", c => c.Long());
            AlterColumn("dbo.IpCheck", "ContentLength", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IpCheck", "ContentLength", c => c.Int());
            AlterColumn("dbo.IpCheck", "ResSpan", c => c.Int());
        }
    }
}
