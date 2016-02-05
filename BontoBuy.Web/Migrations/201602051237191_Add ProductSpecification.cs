namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductSpecification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSpec",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        SpecificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SpecificationId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductSpec");
        }
    }
}
