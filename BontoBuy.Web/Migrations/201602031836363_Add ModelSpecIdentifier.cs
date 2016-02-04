namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelSpecIdentifier : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification");
            DropPrimaryKey("dbo.ModelSpecViewModels");
            AddColumn("dbo.ModelSpecViewModels", "ModelSpecId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ModelSpecViewModels", "ModelSpecId");
            AddForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification", "SpecificationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification");
            DropPrimaryKey("dbo.ModelSpecViewModels");
            DropColumn("dbo.ModelSpecViewModels", "ModelSpecId");
            AddPrimaryKey("dbo.ModelSpecViewModels", "SpecificationId");
            AddForeignKey("dbo.ModelSpecViewModels", "SpecificationId", "dbo.Specification", "SpecificationId");
        }
    }
}
