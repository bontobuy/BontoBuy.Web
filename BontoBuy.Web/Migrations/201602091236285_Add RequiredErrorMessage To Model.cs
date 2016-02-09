namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredErrorMessageToModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Model", "ModelNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Model", "ModelNumber", c => c.String());
        }
    }
}
