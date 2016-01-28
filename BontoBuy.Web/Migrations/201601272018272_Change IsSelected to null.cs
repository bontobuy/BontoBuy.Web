namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsSelectedtonull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "IsSelected", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "IsSelected", c => c.Byte(nullable: false));
        }
    }
}
