namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBrandFromItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "BrandId", "dbo.Brand");
            DropIndex("dbo.Item", new[] { "BrandId" });
            AddColumn("dbo.Model", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Model", "BrandId");
            AddForeignKey("dbo.Model", "BrandId", "dbo.Brand", "BrandId", cascadeDelete: true);
            DropColumn("dbo.Item", "BrandId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "BrandId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Model", "BrandId", "dbo.Brand");
            DropIndex("dbo.Model", new[] { "BrandId" });
            DropColumn("dbo.Model", "BrandId");
            CreateIndex("dbo.Item", "BrandId");
            AddForeignKey("dbo.Item", "BrandId", "dbo.Brand", "BrandId", cascadeDelete: true);
        }
    }
}
