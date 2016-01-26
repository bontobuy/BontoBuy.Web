namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecificationFkTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specification", "TagId", c => c.Int(nullable: false));
            CreateIndex("dbo.Specification", "TagId");
            AddForeignKey("dbo.Specification", "TagId", "dbo.Tag", "TagId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specification", "TagId", "dbo.Tag");
            DropIndex("dbo.Specification", new[] { "TagId" });
            DropColumn("dbo.Specification", "TagId");
        }
    }
}
