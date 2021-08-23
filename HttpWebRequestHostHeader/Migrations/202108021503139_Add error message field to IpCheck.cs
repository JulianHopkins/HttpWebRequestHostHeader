namespace HttpWebRequestHostHeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdderrormessagefieldtoIpCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IpCheck", "ErrorMessage", c => c.String(maxLength: 3000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IpCheck", "ErrorMessage");
        }
    }
}
