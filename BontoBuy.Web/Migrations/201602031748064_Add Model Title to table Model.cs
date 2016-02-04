namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelTitletotableModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "ModelTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Model", "ModelTitle");
        }
    }
}
