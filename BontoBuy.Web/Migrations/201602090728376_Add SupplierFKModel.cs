namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplierFKModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Model", "SupplierId", c => c.String());
            CreateIndex("dbo.Model", "UserId");
            AddForeignKey("dbo.Model", "UserId", "dbo.Supplier", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Model", "UserId", "dbo.Supplier");
            DropIndex("dbo.Model", new[] { "UserId" });
            DropColumn("dbo.Model", "SupplierId");
            DropColumn("dbo.Model", "UserId");
        }
    }
}
