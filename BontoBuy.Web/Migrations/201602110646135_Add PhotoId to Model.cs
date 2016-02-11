namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoIdtoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "PhotoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Model", "PhotoId");
        }
    }
}
