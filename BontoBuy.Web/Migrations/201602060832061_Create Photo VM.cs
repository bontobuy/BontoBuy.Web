namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePhotoVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoViewModels",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhotoViewModels");
        }
    }
}
