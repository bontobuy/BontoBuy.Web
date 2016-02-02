namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatustoVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brand", "Status", c => c.String());
            AddColumn("dbo.Category", "Status", c => c.String());
            AddColumn("dbo.Item", "Status", c => c.String());
            AddColumn("dbo.Product", "Status", c => c.String());
            AddColumn("dbo.Model", "Status", c => c.String());
            AddColumn("dbo.Specification", "Status", c => c.String());
            AddColumn("dbo.Tag", "Status", c => c.String());
            DropColumn("dbo.Category", "IsSelected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category", "IsSelected", c => c.Boolean());
            DropColumn("dbo.Tag", "Status");
            DropColumn("dbo.Specification", "Status");
            DropColumn("dbo.Model", "Status");
            DropColumn("dbo.Product", "Status");
            DropColumn("dbo.Item", "Status");
            DropColumn("dbo.Category", "Status");
            DropColumn("dbo.Brand", "Status");
        }
    }
}
