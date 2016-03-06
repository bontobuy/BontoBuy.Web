namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePaymentDateinPayment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payment", "PaymentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "PaymentDate", c => c.Int(nullable: false));
        }
    }
}
