namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BrandId);
            
            AddColumn("dbo.Item", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Item", "BrandId");
            AddForeignKey("dbo.Item", "BrandId", "dbo.Brand", "BrandId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "BrandId", "dbo.Brand");
            DropIndex("dbo.Item", new[] { "BrandId" });
            DropColumn("dbo.Item", "BrandId");
            DropTable("dbo.Brand");
        }
    }
}
