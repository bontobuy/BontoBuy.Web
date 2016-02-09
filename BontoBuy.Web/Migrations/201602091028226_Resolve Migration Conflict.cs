namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResolveMigrationConflict : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Model", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Model", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Model", "UserId");
            AddForeignKey("dbo.Model", "UserId", "dbo.Supplier", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Model", "UserId", "dbo.Supplier");
            DropIndex("dbo.Model", new[] { "UserId" });
            DropColumn("dbo.Model", "SupplierId");
            DropColumn("dbo.Model", "UserId");
            DropColumn("dbo.Model", "Price");
        }
    }
}
