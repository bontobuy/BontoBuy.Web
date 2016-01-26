namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specification",
                c => new
                    {
                        SpecificationId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SpecificationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Specification");
        }
    }
}
