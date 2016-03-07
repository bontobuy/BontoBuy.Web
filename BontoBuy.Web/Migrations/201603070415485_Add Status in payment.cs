namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusinpayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "Status");
        }
    }
}
