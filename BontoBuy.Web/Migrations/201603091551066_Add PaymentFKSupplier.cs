namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentFKSupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "SupplierUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payment", "SupplierUserId");
            AddForeignKey("dbo.Payment", "SupplierUserId", "dbo.Supplier", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "SupplierUserId", "dbo.Supplier");
            DropIndex("dbo.Payment", new[] { "SupplierUserId" });
            DropColumn("dbo.Payment", "SupplierUserId");
        }
    }
}
