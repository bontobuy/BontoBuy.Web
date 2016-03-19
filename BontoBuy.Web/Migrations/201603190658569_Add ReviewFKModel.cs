namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewFKModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "ModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Review", "ModelId");
            AddForeignKey("dbo.Review", "ModelId", "dbo.Model", "ModelId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "ModelId", "dbo.Model");
            DropIndex("dbo.Review", new[] { "ModelId" });
            DropColumn("dbo.Review", "ModelId");
        }
    }
}
