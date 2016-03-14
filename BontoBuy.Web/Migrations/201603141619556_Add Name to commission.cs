namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNametocommission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Commission", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Commission", "Name");
        }
    }
}
