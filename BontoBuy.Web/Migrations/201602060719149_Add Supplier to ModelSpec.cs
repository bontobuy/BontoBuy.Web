namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuppliertoModelSpec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModelSpecViewModels", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ModelSpecViewModels", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.ModelSpecViewModels", "UserId");
            AddForeignKey("dbo.ModelSpecViewModels", "UserId", "dbo.Supplier", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelSpecViewModels", "UserId", "dbo.Supplier");
            DropIndex("dbo.ModelSpecViewModels", new[] { "UserId" });
            DropColumn("dbo.ModelSpecViewModels", "SupplierId");
            DropColumn("dbo.ModelSpecViewModels", "UserId");
        }
    }
}
