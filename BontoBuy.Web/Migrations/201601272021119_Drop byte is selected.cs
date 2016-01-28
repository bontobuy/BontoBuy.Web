namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dropbyteisselected : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Category", "IsSelected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category", "IsSelected", c => c.Byte());
        }
    }
}
