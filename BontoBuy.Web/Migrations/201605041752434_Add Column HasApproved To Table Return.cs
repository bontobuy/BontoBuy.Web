namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnHasApprovedToTableReturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Return", "HasApproved", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Return", "HasApproved");
        }
    }
}
