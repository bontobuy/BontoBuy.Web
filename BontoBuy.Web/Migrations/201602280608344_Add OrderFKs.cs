namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderFKs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "ModelId", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "CustomerUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Order", "SupplierUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "ModelId");
            CreateIndex("dbo.Order", "CustomerUserId");
            CreateIndex("dbo.Order", "SupplierUserId");
            AddForeignKey("dbo.Order", "CustomerUserId", "dbo.Customer", "Id");
            AddForeignKey("dbo.Order", "ModelId", "dbo.Model", "ModelId", cascadeDelete: true);
            AddForeignKey("dbo.Order", "SupplierUserId", "dbo.Supplier", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "SupplierUserId", "dbo.Supplier");
            DropForeignKey("dbo.Order", "ModelId", "dbo.Model");
            DropForeignKey("dbo.Order", "CustomerUserId", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "SupplierUserId" });
            DropIndex("dbo.Order", new[] { "CustomerUserId" });
            DropIndex("dbo.Order", new[] { "ModelId" });
            DropColumn("dbo.Order", "SupplierUserId");
            DropColumn("dbo.Order", "CustomerUserId");
            DropColumn("dbo.Order", "ModelId");
            DropColumn("dbo.Order", "CustomerId");
            DropColumn("dbo.Order", "SupplierId");
        }
    }
}
