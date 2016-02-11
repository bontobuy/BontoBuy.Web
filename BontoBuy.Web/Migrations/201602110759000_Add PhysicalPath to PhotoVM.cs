namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhysicalPathtoPhotoVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoViewModels", "PhysicalPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoViewModels", "PhysicalPath");
        }
    }
}
