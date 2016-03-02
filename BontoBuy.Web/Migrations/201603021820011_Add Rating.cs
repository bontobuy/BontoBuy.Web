namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rating");
        }
    }
}
