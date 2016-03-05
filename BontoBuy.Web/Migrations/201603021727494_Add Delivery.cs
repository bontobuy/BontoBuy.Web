namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDelivery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Delivery",
                c => new
                    {
                        DeliveryId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ExpectedDeliveryDate = c.DateTime(nullable: false),
                        ActualDeliveryDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Delivery", "OrderId", "dbo.Order");
            DropIndex("dbo.Delivery", new[] { "OrderId" });
            DropTable("dbo.Delivery");
        }
    }
}
