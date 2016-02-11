namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoModel",
                c => new
                    {
                        PhotoId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhotoId, t.ModelId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhotoModel");
        }
    }
}
