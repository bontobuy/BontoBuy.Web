namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Termsandconditions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "TermsAndConditions", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "TermsAndConditions");
        }
    }
}
