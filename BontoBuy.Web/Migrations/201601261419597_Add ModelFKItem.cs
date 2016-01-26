namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelFKItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "ItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.Model", "ItemId");
            AddForeignKey("dbo.Model", "ItemId", "dbo.Item", "ItemId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Model", "ItemId", "dbo.Item");
            DropIndex("dbo.Model", new[] { "ItemId" });
            DropColumn("dbo.Model", "ItemId");
        }
    }
}
