namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemFKProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "ItemProductViewModel_ProductId", "dbo.Product");
            DropIndex("dbo.Item", new[] { "ItemProductViewModel_ProductId" });
            RenameColumn(table: "dbo.Item", name: "ItemProductViewModel_ProductId", newName: "ProductId");
            AlterColumn("dbo.Item", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Item", "ProductId");
            AddForeignKey("dbo.Item", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "ProductId", "dbo.Product");
            DropIndex("dbo.Item", new[] { "ProductId" });
            AlterColumn("dbo.Item", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Item", name: "ProductId", newName: "ItemProductViewModel_ProductId");
            CreateIndex("dbo.Item", "ItemProductViewModel_ProductId");
            AddForeignKey("dbo.Item", "ItemProductViewModel_ProductId", "dbo.Product", "ProductId");
        }
    }
}
