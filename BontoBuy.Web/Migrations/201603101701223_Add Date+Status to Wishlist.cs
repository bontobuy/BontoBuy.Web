namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateStatustoWishlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wishlist", "DtCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Wishlist", "DtUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Wishlist", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wishlist", "Status");
            DropColumn("dbo.Wishlist", "DtUpdated");
            DropColumn("dbo.Wishlist", "DtCreated");
        }
    }
}
