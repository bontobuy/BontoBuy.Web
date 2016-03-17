namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfirmationCodetoOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ConfirmationCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "ConfirmationCode");
        }
    }
}
