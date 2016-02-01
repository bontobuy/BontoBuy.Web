namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification");
            DropPrimaryKey("dbo.ModelSpecViewModels");
            AddColumn("dbo.ModelSpecViewModels", "Description", c => c.String());
            AddPrimaryKey("dbo.ModelSpecViewModels", "SpecificationId");
            AddForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification", "SpecificationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification");
            DropPrimaryKey("dbo.ModelSpecViewModels");
            DropColumn("dbo.ModelSpecViewModels", "Description");
            AddPrimaryKey("dbo.ModelSpecViewModels", new[] { "SpecificationId", "ModelId" });
            AddForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification", "SpecificationId", cascadeDelete: true);
        }
    }
}
