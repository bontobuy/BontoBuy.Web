namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveModelPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Model", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Model", "Price", c => c.Int(nullable: false));
        }
    }
}
