namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnUnitPriceToOrderVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UnitPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "UnitPrice");
        }
    }
}
