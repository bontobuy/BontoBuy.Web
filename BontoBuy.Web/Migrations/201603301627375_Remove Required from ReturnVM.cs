namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredfromReturnVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Return", "ReturnMethod", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Return", "ReturnMethod", c => c.String(nullable: false));
        }
    }
}
