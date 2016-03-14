namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRatingFromModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Model", "RatingId", "dbo.Rating");
            DropIndex("dbo.Model", new[] { "RatingId" });
            DropColumn("dbo.Model", "RatingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Model", "RatingId", c => c.Int());
            CreateIndex("dbo.Model", "RatingId");
            AddForeignKey("dbo.Model", "RatingId", "dbo.Rating", "RatingId");
        }
    }
}
