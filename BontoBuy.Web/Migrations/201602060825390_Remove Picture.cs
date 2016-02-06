namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePicture : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Picture");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Picture",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.PictureId);
            
        }
    }
}
