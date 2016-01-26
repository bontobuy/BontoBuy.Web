namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddModelSpecificationManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelSpecification",
                c => new
                    {
                        SpecificationViewModel_SpecificationId = c.Int(nullable: false),
                        ModelViewModel_ModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpecificationViewModel_SpecificationId, t.ModelViewModel_ModelId })
                .ForeignKey("dbo.Specification", t => t.SpecificationViewModel_SpecificationId, cascadeDelete: true)
                .ForeignKey("dbo.Model", t => t.ModelViewModel_ModelId, cascadeDelete: true)
                .Index(t => t.SpecificationViewModel_SpecificationId)
                .Index(t => t.ModelViewModel_ModelId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SpecificationViewModelModelViewModels", "ModelViewModel_ModelId", "dbo.Model");
            DropForeignKey("dbo.SpecificationViewModelModelViewModels", "SpecificationViewModel_SpecificationId", "dbo.Specification");
            DropIndex("dbo.SpecificationViewModelModelViewModels", new[] { "ModelViewModel_ModelId" });
            DropIndex("dbo.SpecificationViewModelModelViewModels", new[] { "SpecificationViewModel_SpecificationId" });
            DropTable("dbo.SpecificationViewModelModelViewModels");
        }
    }
}