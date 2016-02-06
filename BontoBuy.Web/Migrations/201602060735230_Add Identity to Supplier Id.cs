namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentitytoSupplierId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplier", "SupplierId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplier", "SupplierId", c => c.Int(nullable: false));
        }
    }
}
