namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableToColumnDateUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Delivery", "ExpectedDeliveryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Delivery", "ActualDeliveryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Delivery", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Delivery", "DateUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Delivery", "DateUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Delivery", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Delivery", "ActualDeliveryDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Delivery", "ExpectedDeliveryDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
