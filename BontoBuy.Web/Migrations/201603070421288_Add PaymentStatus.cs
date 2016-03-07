namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentStatus");
        }
    }
}
