namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnNotificationtoReturnVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Return", "Notification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Return", "Notification");
        }
    }
}
