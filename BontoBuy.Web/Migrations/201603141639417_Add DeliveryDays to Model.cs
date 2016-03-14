namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliveryDaystoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "DeliveryInDays", c => c.Int(nullable: false));
            AddColumn("dbo.Model", "NumberDaysToAdvert", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Model", "NumberDaysToAdvert");
            DropColumn("dbo.Model", "DeliveryInDays");
        }
    }
}
