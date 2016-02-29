namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatusViewModels",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderStatusViewModels");
        }
    }
}
