namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAdmin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Admin", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Admin", new[] { "Id" });
            DropTable("dbo.Admin");
        }
    }
}