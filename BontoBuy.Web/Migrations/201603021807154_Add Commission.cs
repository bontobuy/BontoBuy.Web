namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false, identity: true),
                        Percentage = c.Int(nullable: false),
                        DtCreated = c.DateTime(nullable: false),
                        DtUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Commission");
        }
    }
}
