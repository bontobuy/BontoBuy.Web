namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddCommissionFKToSupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplier", "CommissionId", c => c.Int(nullable: true));
            CreateIndex("dbo.Supplier", "CommissionId");
            AddForeignKey("dbo.Supplier", "CommissionId", "dbo.Commission", "CommissionId", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Supplier", "CommissionId", "dbo.Commission");
            DropIndex("dbo.Supplier", new[] { "CommissionId" });
            DropColumn("dbo.Supplier", "CommissionId");
        }
    }
}