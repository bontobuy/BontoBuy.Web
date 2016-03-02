namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingFKModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "RatingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Model", "RatingId");
            AddForeignKey("dbo.Model", "RatingId", "dbo.Rating", "RatingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Model", "RatingId", "dbo.Rating");
            DropIndex("dbo.Model", new[] { "RatingId" });
            DropColumn("dbo.Model", "RatingId");
        }
    }
}
