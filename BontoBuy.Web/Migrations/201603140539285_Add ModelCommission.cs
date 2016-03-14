namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelCommission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelCommission",
                c => new
                    {
                        ModelCommissionId = c.Int(nullable: false, identity: true),
                        ModelId = c.Int(nullable: false),
                        CommissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ModelCommissionId, t.ModelId, t.CommissionId })
                .ForeignKey("dbo.Commission", t => t.CommissionId, cascadeDelete: true)
                .ForeignKey("dbo.Model", t => t.ModelId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.CommissionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelCommission", "ModelId", "dbo.Model");
            DropForeignKey("dbo.ModelCommission", "CommissionId", "dbo.Commission");
            DropIndex("dbo.ModelCommission", new[] { "CommissionId" });
            DropIndex("dbo.ModelCommission", new[] { "ModelId" });
            DropTable("dbo.ModelCommission");
        }
    }
}
