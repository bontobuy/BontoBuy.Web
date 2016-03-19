namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasReturntoOrderVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "HasReturn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "HasReturn");
        }
    }
}
