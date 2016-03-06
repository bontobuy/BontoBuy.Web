namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityToAdminId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admin", "AdminId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admin", "AdminId", c => c.Int(nullable: false));
        }
    }
}
