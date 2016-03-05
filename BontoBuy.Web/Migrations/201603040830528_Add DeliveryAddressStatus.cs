namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliveryAddressStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryAddressStatus",
                c => new
                    {
                        DeliveryAddressStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryAddressStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeliveryAddressStatus");
        }
    }
}
