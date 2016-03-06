namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnQuantityToOrderVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Quantity");
        }
    }
}
