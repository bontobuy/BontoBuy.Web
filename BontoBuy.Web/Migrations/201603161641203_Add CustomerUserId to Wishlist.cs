namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerUserIdtoWishlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wishlist", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Wishlist", "UserId");
            AddForeignKey("dbo.Wishlist", "UserId", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wishlist", "UserId", "dbo.Customer");
            DropIndex("dbo.Wishlist", new[] { "UserId" });
            DropColumn("dbo.Wishlist", "UserId");
        }
    }
}
