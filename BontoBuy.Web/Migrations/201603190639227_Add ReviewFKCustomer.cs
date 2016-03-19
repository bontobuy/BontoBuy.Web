namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewFKCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Review", "UserId");
            AddForeignKey("dbo.Review", "UserId", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "UserId", "dbo.Customer");
            DropIndex("dbo.Review", new[] { "UserId" });
            DropColumn("dbo.Review", "UserId");
        }
    }
}
