namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Model",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ModelId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Model");
        }
    }
}
