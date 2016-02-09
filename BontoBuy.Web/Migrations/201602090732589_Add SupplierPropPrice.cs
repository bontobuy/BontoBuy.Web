namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplierPropPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Model", "Price");
        }
    }
}
