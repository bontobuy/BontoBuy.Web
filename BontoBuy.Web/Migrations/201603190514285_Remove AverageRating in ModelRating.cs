namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAverageRatinginModelRating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RatingModel", "AverageRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RatingModel", "AverageRating", c => c.Int(nullable: false));
        }
    }
}
