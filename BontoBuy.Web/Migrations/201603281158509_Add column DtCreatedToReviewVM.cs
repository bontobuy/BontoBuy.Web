namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnDtCreatedToReviewVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "DtCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "DtCreated");
        }
    }
}
