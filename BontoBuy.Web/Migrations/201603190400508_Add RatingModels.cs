namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingModel",
                c => new
                    {
                        RatingModelId = c.Int(nullable: false, identity: true),
                        ModelId = c.Int(nullable: false),
                        RatingId = c.Int(nullable: false),
                        AverageRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RatingModelId, t.ModelId, t.RatingId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatingModel");
        }
    }
}
