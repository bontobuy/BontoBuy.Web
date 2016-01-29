namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrandItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Brand", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Brand");
        }
    }
}
