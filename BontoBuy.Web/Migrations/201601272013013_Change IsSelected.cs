namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsSelected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "IsSelected", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "IsSelected");
        }
    }
}
