namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityToCustomerId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "CustomerId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "CustomerId", c => c.Int(nullable: false));
        }
    }
}
