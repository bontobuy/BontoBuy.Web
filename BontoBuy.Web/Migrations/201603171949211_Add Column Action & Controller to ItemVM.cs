namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnActionControllertoItemVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Action", c => c.String());
            AddColumn("dbo.Item", "Controller", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Controller");
            DropColumn("dbo.Item", "Action");
        }
    }
}
