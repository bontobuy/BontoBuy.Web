namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tag");
        }
    }
}
