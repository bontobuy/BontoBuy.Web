namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addboolisselected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "IsSelected", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "IsSelected");
        }
    }
}
