namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "OrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Payment", "CommissionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payment", "OrderId");
            CreateIndex("dbo.Payment", "CommissionId");
            AddForeignKey("dbo.Payment", "CommissionId", "dbo.Commission", "CommissionId", cascadeDelete: true);
            AddForeignKey("dbo.Payment", "OrderId", "dbo.Order", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Payment", "CommissionId", "dbo.Commission");
            DropIndex("dbo.Payment", new[] { "CommissionId" });
            DropIndex("dbo.Payment", new[] { "OrderId" });
            DropColumn("dbo.Payment", "CommissionId");
            DropColumn("dbo.Payment", "OrderId");
        }
    }
}
