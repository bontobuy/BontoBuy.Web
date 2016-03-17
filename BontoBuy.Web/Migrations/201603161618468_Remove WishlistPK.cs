namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveWishlistPK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Wishlist");
            AddPrimaryKey("dbo.Wishlist", "WishlistId");
            DropColumn("dbo.Wishlist", "ModelId");
            DropColumn("dbo.Wishlist", "UserId");
            DropColumn("dbo.Wishlist", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wishlist", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Wishlist", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Wishlist", "ModelId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Wishlist");
            AddPrimaryKey("dbo.Wishlist", new[] { "ModelId", "UserId", "WishlistId" });
        }
    }
}
