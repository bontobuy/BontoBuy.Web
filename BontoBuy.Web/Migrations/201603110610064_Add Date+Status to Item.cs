namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateStatustoItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "AdminStatus", c => c.String());
            AddColumn("dbo.Item", "DtCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Item", "DtUpdated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Item", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "Status", c => c.String());
            DropColumn("dbo.Item", "DtUpdated");
            DropColumn("dbo.Item", "DtCreated");
            DropColumn("dbo.Item", "AdminStatus");
        }
    }
}
