namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatustoDeliveryAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryAddress", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryAddress", "Status");
        }
    }
}
