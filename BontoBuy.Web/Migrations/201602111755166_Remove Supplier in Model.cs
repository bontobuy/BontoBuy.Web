namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSupplierinModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Model", "SupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Model", "SupplierId", c => c.Int(nullable: false));
        }
    }
}
