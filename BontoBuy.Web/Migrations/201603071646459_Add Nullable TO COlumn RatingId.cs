namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableTOCOlumnRatingId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Model", "RatingId", "dbo.Rating");
            DropIndex("dbo.Model", new[] { "RatingId" });
            AlterColumn("dbo.Model", "RatingId", c => c.Int());
            CreateIndex("dbo.Model", "RatingId");
            AddForeignKey("dbo.Model", "RatingId", "dbo.Rating", "RatingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Model", "RatingId", "dbo.Rating");
            DropIndex("dbo.Model", new[] { "RatingId" });
            AlterColumn("dbo.Model", "RatingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Model", "RatingId");
            AddForeignKey("dbo.Model", "RatingId", "dbo.Rating", "RatingId", cascadeDelete: true);
        }
    }
}
