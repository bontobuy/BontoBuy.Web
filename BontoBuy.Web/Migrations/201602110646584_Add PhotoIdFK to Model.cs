namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoIdFKtoModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Model", "PhotoId");
            AddForeignKey("dbo.Model", "PhotoId", "dbo.PhotoViewModels", "PhotoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Model", "PhotoId", "dbo.PhotoViewModels");
            DropIndex("dbo.Model", new[] { "PhotoId" });
        }
    }
}
