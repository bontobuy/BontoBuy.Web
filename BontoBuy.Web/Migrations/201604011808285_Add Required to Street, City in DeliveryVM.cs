namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddRequiredtoStreetCityinDeliveryVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryAddress", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.DeliveryAddress", "City", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.DeliveryAddress", "City", c => c.String());
            AlterColumn("dbo.DeliveryAddress", "Street", c => c.String());
        }
    }
}