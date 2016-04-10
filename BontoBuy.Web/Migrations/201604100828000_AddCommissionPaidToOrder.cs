namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommissionPaidToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CommissionPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CommissionPaid");
        }
    }
}
