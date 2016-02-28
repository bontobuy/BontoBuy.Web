namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddOrderVm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        DtCreated = c.DateTime(nullable: false),
                        ExpectedDeliveryDate = c.DateTime(nullable: false),
                        RealDeliveryDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        Total = c.Int(nullable: false),
                        GrandTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
        }

        public override void Down()
        {
            DropTable("dbo.Order");
        }
    }
}