namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "ModelNumber", c => c.String());
            AddColumn("dbo.Specification", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Model", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Model", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Specification", "Price");
            DropColumn("dbo.Model", "ModelNumber");
        }
    }
}
