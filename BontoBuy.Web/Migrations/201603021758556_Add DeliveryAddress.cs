namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliveryAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryAddress",
                c => new
                    {
                        DeliveryAddressId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CustomerId = c.Int(nullable: false),
                        Street = c.String(),
                        Zipcode = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryAddressId)
                .ForeignKey("dbo.Customer", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Return",
                c => new
                    {
                        ReturnId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        ReturnMethod = c.String(),
                        Reason = c.String(),
                        Status = c.String(),
                        DtCreated = c.DateTime(nullable: false),
                        DtUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReturnId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Return", "OrderId", "dbo.Order");
            DropForeignKey("dbo.DeliveryAddress", "UserId", "dbo.Customer");
            DropIndex("dbo.Return", new[] { "OrderId" });
            DropIndex("dbo.DeliveryAddress", new[] { "UserId" });
            DropTable("dbo.Return");
            DropTable("dbo.DeliveryAddress");
        }
    }
}
