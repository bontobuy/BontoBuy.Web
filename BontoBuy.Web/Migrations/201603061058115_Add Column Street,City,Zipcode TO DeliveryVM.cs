namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnStreetCityZipcodeTODeliveryVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Delivery", "Street", c => c.String());
            AddColumn("dbo.Delivery", "City", c => c.String());
            AddColumn("dbo.Delivery", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Delivery", "Zipcode");
            DropColumn("dbo.Delivery", "City");
            DropColumn("dbo.Delivery", "Street");
        }
    }
}
