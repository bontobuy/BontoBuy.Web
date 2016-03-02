namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PaymentDate = c.Int(nullable: false),
                        DtCreated = c.DateTime(nullable: false),
                        DtUpdated = c.DateTime(nullable: false),
                        DiscountAllowed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payment");
        }
    }
}
