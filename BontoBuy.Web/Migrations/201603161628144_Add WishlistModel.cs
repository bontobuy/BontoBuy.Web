namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWishlistModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishlistModel",
                c => new
                    {
                        WishlistId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WishlistId, t.ModelId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WishlistModel");
        }
    }
}
