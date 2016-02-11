namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRefPhotoModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Model", "PhotoId", "dbo.PhotoViewModels");
            DropIndex("dbo.Model", new[] { "PhotoId" });
            DropColumn("dbo.Model", "PhotoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Model", "PhotoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Model", "PhotoId");
            AddForeignKey("dbo.Model", "PhotoId", "dbo.PhotoViewModels", "PhotoId", cascadeDelete: true);
        }
    }
}
