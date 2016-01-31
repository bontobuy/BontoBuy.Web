namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditModelSpec : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ModelSpecViewModels");
            AddColumn("dbo.ModelSpecViewModels", "SpecificationId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ModelSpecViewModels", "SpecificationId");
            CreateIndex("dbo.ModelSpecViewModels", "SpecificationId");
            AddForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification", "SpecificationId");
            DropColumn("dbo.ModelSpecViewModels", "ModelSpecId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelSpecViewModels", "ModelSpecId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification");
            DropIndex("dbo.ModelSpecViewModels", new[] { "SpecificationId" });
            DropPrimaryKey("dbo.ModelSpecViewModels");
            DropColumn("dbo.ModelSpecViewModels", "SpecificationId");
            AddPrimaryKey("dbo.ModelSpecViewModels", "ModelSpecId");
        }
    }
}
