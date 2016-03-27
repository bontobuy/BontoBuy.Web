namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddrequiredtoReturnVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Return", "ReturnMethod", c => c.String(nullable: false));
            AlterColumn("dbo.Return", "Reason", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Return", "Reason", c => c.String());
            AlterColumn("dbo.Return", "ReturnMethod", c => c.String());
        }
    }
}
