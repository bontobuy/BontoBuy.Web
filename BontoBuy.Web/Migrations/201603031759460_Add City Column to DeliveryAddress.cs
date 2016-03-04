namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityColumntoDeliveryAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryAddress", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryAddress", "City");
        }
    }
}
