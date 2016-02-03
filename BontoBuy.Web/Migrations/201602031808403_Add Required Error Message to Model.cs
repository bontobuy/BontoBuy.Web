namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredErrorMessagetoModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Model", "ModelTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Model", "ModelTitle", c => c.String());
        }
    }
}
