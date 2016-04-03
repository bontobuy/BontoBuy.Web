namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredtoDescriptionInreviewViewModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Review", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Review", "Description", c => c.String());
        }
    }
}
