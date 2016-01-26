namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ItemProductViewModel_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Product", t => t.ItemProductViewModel_ProductId)
                .Index(t => t.ItemProductViewModel_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "ItemProductViewModel_ProductId", "dbo.Product");
            DropIndex("dbo.Item", new[] { "ItemProductViewModel_ProductId" });
            DropTable("dbo.Item");
        }
    }
}
