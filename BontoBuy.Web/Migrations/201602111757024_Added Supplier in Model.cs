namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSupplierinModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "SupplierId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Model", "SupplierId");
        }
    }
}
