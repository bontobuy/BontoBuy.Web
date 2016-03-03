namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wishlist",
                c => new
                    {
                        ModelId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        WishlistId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.ModelId, t.UserId, t.WishlistId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Wishlist");
        }
    }
}
