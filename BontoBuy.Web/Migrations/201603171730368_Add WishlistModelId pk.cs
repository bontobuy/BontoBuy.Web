namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWishlistModelIdpk : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.WishlistModel");
            AddColumn("dbo.WishlistModel", "WishlistModelId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.WishlistModel", new[] { "WishlistModelId", "WishlistId", "ModelId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.WishlistModel");
            DropColumn("dbo.WishlistModel", "WishlistModelId");
            AddPrimaryKey("dbo.WishlistModel", new[] { "WishlistId", "ModelId" });
        }
    }
}
