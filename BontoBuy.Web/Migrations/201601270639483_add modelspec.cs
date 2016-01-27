namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addmodelspec : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelSpecViewModels",
                c => new
                    {
                        SpecificationId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.SpecificationId, t.ModelId })
                .ForeignKey("dbo.Model", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.Specification", t => t.SpecificationId, cascadeDelete: true)
                .Index(t => t.SpecificationId)
                .Index(t => t.ModelId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification");
            DropForeignKey("dbo.ModelSpecViewModels", "ModelId", "dbo.Model");
            DropIndex("dbo.ModelSpecViewModels", new[] { "ModelId" });
            DropIndex("dbo.ModelSpecViewModels", new[] { "SpecificationId" });
            DropTable("dbo.ModelSpecViewModels");
        }
    }
}