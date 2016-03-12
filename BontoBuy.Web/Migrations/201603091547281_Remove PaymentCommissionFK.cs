namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePaymentCommissionFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payment", "CommissionId", "dbo.Commission");
            DropIndex("dbo.Payment", new[] { "CommissionId" });
            DropColumn("dbo.Payment", "CommissionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "CommissionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payment", "CommissionId");
            AddForeignKey("dbo.Payment", "CommissionId", "dbo.Commission", "CommissionId", cascadeDelete: true);
        }
    }
}
