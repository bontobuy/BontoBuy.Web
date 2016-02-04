namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SupplierId = c.Int(nullable: false),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplier", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Supplier", new[] { "Id" });
            DropTable("dbo.Supplier");
        }
    }
}
